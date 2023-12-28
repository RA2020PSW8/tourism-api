using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.MarketPlace
{
    public interface ITourRecommendationService
    {
        public Result<PagedResult<TourDto>> GetRecommendedToursAI(int page, int pageSize, int id);
        public Result<PagedResult<TourDto>> GetRecommendedToursByKeypoints(double latitude, double longitude);

        public Result<PagedResult<TourDto>> GetRecommendedTours(double latitude, double longitutde, long id);
        public Result<PagedResult<TourDto>> GetRecommendedActiveTours(double latitude, double longitude); 

        public Result<PagedResult<KeyValuePair<TourDto,double>>> GetRecommendedCommunityTours(double latitude, double longitude, long id, long tourId);
    }
}
