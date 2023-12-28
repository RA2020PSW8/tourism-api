using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Enum;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

using FluentResults;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Scripting.Hosting;
using Accord.MachineLearning;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System.Text.RegularExpressions;

namespace Explorer.Tours.Core.UseCases.MarketPlace
{
    public class TourRecommendationService : BaseService<TourDto, Tour>, ITourRecommendationService
    {

   
        protected readonly ITourReviewRepository _reviewRepository;
        protected readonly ITourRepository _tourRepository;
        protected readonly IKeypointRepository _keyPointRepository;
        protected readonly ITourPreferenceRepository _tourPreferenceRepository; 
        protected readonly ITouristPositionRepository _touristPositionRepository;
        protected readonly ITourProgressRepository _tourProgressRepository;
        protected readonly ITourPurchaseTokenRepository _tourPurchaseTokenRepository;
     
        private string _apiDirectory;

        public TourRecommendationService(
            ITourReviewRepository tourReviewRepository, ITourRepository tourRepository, ITourPreferenceRepository tourPreferenceRepository, ITouristPositionRepository touristPositionRepository,  
            IKeypointRepository keyPointRepository,ITourProgressRepository tourProgressRepository, ITourPurchaseTokenRepository tourPurchaseTokenRepository,IMapper mapper) : base(mapper)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _apiDirectory = GetRootDir();
            _apiDirectory += "\\Modules\\Tours\\Explorer.Tours.Core\\UseCases\\MarketPlace\\recommendation-system";


            _tourPreferenceRepository = tourPreferenceRepository;
            _touristPositionRepository = touristPositionRepository;
            _reviewRepository = tourReviewRepository;
            _tourRepository = tourRepository;
            _keyPointRepository = keyPointRepository;
            _tourProgressRepository = tourProgressRepository;
            _tourPurchaseTokenRepository = tourPurchaseTokenRepository; 
        }

        public Result<PagedResult<TourDto>> GetRecommendedToursAI(int page, int pageSize, int id)
        {
        
          
            //var ratingData = _reviewRepository.GetPaged(0, 0).Results;
            
            //GenerateCsvFiles(ratingData);
            List<long> tourIds = RunRecommendationAlgorithm(id);


        
            var pagedResult = _tourRepository.GetByIds(page, pageSize, tourIds); 

           
           

            return MapToDto(pagedResult); 



        }


        public Result<PagedResult<TourDto>> GetRecommendedTours(double latitude, double longitude, long id) {

            var ratingData = _reviewRepository.GetPaged(0, 0).Results;

            var popularTours = ratingData
            .GroupBy(review => review.TourId)
            .Where(group => group.Count() >= 50)
            .Select(group => group.Key);

           
            var toursPaged = _tourRepository.GetPaged(0, 0);
            var tours = _tourRepository.GetPaged(0, 0).Results;

            var preference = _tourPreferenceRepository.GetByUser(id);

            var userPreferenceSimilarityScores = new Dictionary<long, double>();
            var tourPreferenceSimliarityScores = new Dictionary<long, double>(); 
 
            if (preference != null) {


                var userVector = new List<int> { 1, 1};

                foreach (var tag in preference.Tags)
                {
                    userVector.Add(1);
                }

                var tourVectors = new Dictionary<long, List<int>>();

                foreach (var tour in tours) {
                    var tourVector = new List<int> {
                        tour.TransportType == preference.TransportType ? 1 : 0,
                        tour.Difficulty == preference.Difficulty ? 1 : 0};


                    foreach (var tag in preference.Tags) {
                        tourVector.Add(tour.Tags.Contains(tag) ? 1 : 0);

                    }
                    tourVectors.Add(tour.Id, tourVector); 

                }



                foreach (var tourVector in tourVectors)
                {
                    double similarity = MatchingPercentage(userVector, tourVector.Value);
                    userPreferenceSimilarityScores.Add(tourVector.Key, similarity);
                }





            }

            var finishedTours = _tourProgressRepository.GetCompletedByUser(id);
            if (finishedTours != null)
            {
                var tourIds = new List<long>();
                foreach (var f in finishedTours)
                {
                    tourIds.Add(f.TourId);
                }

                var last10Tours = _tourRepository.GetByIds(0, 0, tourIds).Results;

                var tagVector = new Dictionary<string, int>();

                var allTags = last10Tours
                            .SelectMany(t => t.Tags) 
                            .Distinct() 
                            .ToList();

                var tourVectors = new Dictionary<long, List<int>>();

                foreach (var tour in tours) {
                    tourVectors.Add(tour.Id,allTags.Select(tag => tour.Tags.Contains(tag) ? 1 : 0).ToList());

                }


                foreach (var tourVector in tourVectors)
                {
                    double similarity = MatchingPercentage(Enumerable.Repeat(1, allTags.Count()).ToList(), tourVector.Value); ;
                    tourPreferenceSimliarityScores.Add(tourVector.Key, similarity);
                }




            }
           
            
            var mergedScores = new Dictionary<long, double>();
            if (userPreferenceSimilarityScores.Count > 0 && tourPreferenceSimliarityScores.Count > 0) {
                foreach (var key in userPreferenceSimilarityScores.Keys)
                {
                    if (tourPreferenceSimliarityScores.TryGetValue(key, out double tourScore))
                    {
                        double userScore = userPreferenceSimilarityScores[key];
                        double averageScore = (userScore + tourScore) / 2.0;
                        mergedScores[key] = averageScore;
                    }
                }
            } else if(tourPreferenceSimliarityScores.Count >0) {
                mergedScores = tourPreferenceSimliarityScores; 
            }
            else if ( userPreferenceSimilarityScores.Count>0)
            {
                mergedScores = userPreferenceSimilarityScores; 
            }

            var keysToRemove = mergedScores.Keys.Except(popularTours).ToList();
            foreach (var key in keysToRemove)
            {
                mergedScores.Remove(key);
            }

            var sortedScores = mergedScores.OrderByDescending(x => x.Value).ToList();

           
            var first10Indices = sortedScores.Take(10).Select(x => x.Key).ToList();



            var recommendedTours = _tourRepository.GetByIds(0, 10, first10Indices); 




            return MapToDto(recommendedTours); 








        }

        public static double MatchingPercentage(List<int> list1, List<int> list2)
        {
            if (list1.Count != list2.Count)
            {
                throw new ArgumentException("Lists must have the same length.");
            }

            int totalElements = list1.Count;

            int matchingElements = list1.Where((val, index) => val == list2[index]).Count();

            double matchingPercentage = (double)matchingElements / totalElements;

            return matchingPercentage;
        }

       

        public Result<PagedResult<TourDto>> GetRecommendedActiveTours(double latitude, double longitutde)
        {

            List<long> activeIds = _tourProgressRepository.GetActiveTours().Select(t => t.TourId).Distinct().ToList();

            var ratingData = _reviewRepository.GetPaged(0, 0).Results
              .Where(review => activeIds.Contains(review.TourId)) 
              .Where(review => review.RatingDate >= DateTime.Now.AddDays(-7)) 
              .GroupBy(review => review.TourId) 
              .Select(group => new {
                  TourId = group.Key,
                  ReviewCount = group.Count(),
                  AverageRating = group.Average(review => review.Rating)
              })
              .ToList();

            var tokens = _tourPurchaseTokenRepository.GetPaged(0, 0).Results; 

            var groupTokens = tokens
                .Where(token => activeIds.Contains(token.TourId))
                .GroupBy(token => token.TourId)
                .Select(group => new
                {
                   TourId = group.Key, 
                   TokenCount = group.Count(),
                })
                .ToList();

            var joinedData = (from rating in ratingData
                              join token in groupTokens
                              on rating.TourId equals token.TourId into temp
                              from t in temp.DefaultIfEmpty()
                              select new
                              {
                                  TourId = rating.TourId,
                                  ReviewCount = rating.ReviewCount,
                                  AverageRating = rating.AverageRating,
                                  TokenCount = (t != null) ? t.TokenCount : 0
                              })
                    .ToList();

            var tourScores = joinedData.Select(t => new
            {
                TourId = t.TourId,
                Score = (t.ReviewCount * 0.4) + (t.AverageRating * 0.3) + (t.TokenCount * 0.3)
            }).ToDictionary(x => x.TourId, x => x.Score);

          
            var topTourIds = tourScores.OrderByDescending(x => x.Value)
                                       .Take(10)
                                       .Select(x => x.Key)
                                       .ToList();

            return MapToDto(_tourRepository.GetByIds(0, 10, topTourIds)); 

        }
    
        public List<long> RunRecommendationAlgorithm(int id)
        {

            int userIdToFind = id;


            string pythonScriptPath = Path.Combine(_apiDirectory, "predict.py");


            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";
            start.Arguments = $"{pythonScriptPath} {userIdToFind}";
            start.WorkingDirectory = Path.GetDirectoryName(pythonScriptPath);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;


            List<long> recommendations = new List<long>();

            using (Process process = Process.Start(start))
            {
                using (System.IO.StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();

                    recommendations = ParseRecommendations(result);

                }
            }
            return recommendations;



        }
        private string GetRootDir() {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;


            DirectoryInfo rootDirectory = new DirectoryInfo(currentDirectory);
            while (rootDirectory.Name != "src") 
            {
                rootDirectory = rootDirectory.Parent;
                if (rootDirectory == null)
                {
                    return "";
                }
            }

            return rootDirectory.FullName;

        }
        private List<long> ParseRecommendations(string output)
        {

            List<long> recommendations = new List<long>();
            string[] parts = output.Split(new char[] { '[', ']', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                if (int.TryParse(part.Trim(), out int recommendation))
                {
                    recommendations.Add(recommendation);
                }
            }
            return recommendations;
        }


        private void GenerateCsvFiles(List<TourReview> ratings) {





            string[] result = GenerateCsvContent(ratings);

          
            CreateCsvFile("ratings.csv", "UserId,TourId,Rating", result[0]);






        }
       

        private string[] GenerateCsvContent(List<TourReview> ratings) {
           
            var ratingsCsv = ratings.Select(obj => new
            {
                UserId = obj.UserId,
                TourId = obj.TourId,
                Rating = obj.Rating

            });

            string ratingsCsvContent = string.Join(Environment.NewLine, ratingsCsv.Select(o => $"{o.UserId},{o.TourId},{o.Rating}"));

            return new string[] { ratingsCsvContent };
        }
        private void CreateCsvFile(string file_name, string features, string csv_content) {
            string filePath = Path.Combine(_apiDirectory, file_name);


            File.WriteAllText(filePath, features + "\n" + csv_content);

        }

        public Result<PagedResult<TourDto>> GetRecommendedToursByKeypoints(double latitude, double longitude) {

            List<long> idices = FindKeyPoints(latitude, longitude);

            var tours = _tourRepository.GetPaged(0,0).Results.Where(t => idices.Contains(t.Id)).ToList();
            var pagedTours = new PagedResult<Tour>(tours, tours.Count()); 
            
            
            return  MapToDto(pagedTours);

             


        }

        public List<long> FindKeyPoints(double latitude, double longitude)
        {
            var keypoints = _keyPointRepository.GetPaged(0, 0).Results;
            var res = keypoints.Where(k => DistanceCalculator.CalculateDistance(latitude, longitude, k.Latitude, k.Longitude) <= 0.1);            

            return res.Select(k => k.TourId).ToList(); 


            
        }

        
    }
}
