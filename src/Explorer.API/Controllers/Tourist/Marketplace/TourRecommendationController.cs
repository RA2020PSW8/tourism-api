using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Marketplace
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/marketplace/tours/")]
    public class TourRecommendationController: BaseApiController
    {
        private readonly ITourRecommendationService _tourRecommendationService;

        public TourRecommendationController(ITourRecommendationService tourRecommendationService)
        {
            _tourRecommendationService = tourRecommendationService;
        }   

        [HttpGet("recommended-tours-ai/{id:int}")]
        public ActionResult<List<TourDto>> GetRecommendedToursAI([FromQuery] int page, [FromQuery] int pageSize,  int id)
        {

              var result = _tourRecommendationService.GetRecommendedToursAI(page, pageSize, id); 
              return CreateResponse(result);
        }

        [HttpGet("recommended-tours/{id:int}")]
        public ActionResult<PagedResult<TourDto>> GetTourRecommendations([FromQuery] double latitude, [FromQuery] double longitude,  long id)
        {

            var result = _tourRecommendationService.GetRecommendedTours(latitude, longitude, id);

            return CreateResponse(result); 
        

        }
        [HttpGet("recommended-tours-active/")]
        public ActionResult<PagedResult<TourDto>> GetActiveTourRecommendations([FromQuery] double latitude, [FromQuery] double longitude) {

            var result = _tourRecommendationService.GetRecommendedActiveTours(latitude, longitude);

            return CreateResponse(result); 
        }


        [HttpGet("recommended-tours-kp")]
        public ActionResult<PagedResult<TourDto>> GetRecommendedToursByKeypoints([FromQuery] double latitude, [FromQuery]double longitude)
        {
            var result = _tourRecommendationService.GetRecommendedToursByKeypoints(latitude, longitude);
            return CreateResponse(result); 
        
        }
    }
}
