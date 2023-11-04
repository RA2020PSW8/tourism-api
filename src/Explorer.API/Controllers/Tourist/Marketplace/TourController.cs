using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Marketplace;

[Authorize(Policy = "touristPolicy")]
[Route("api/marketplace/tours/")]
public class TourController : BaseApiController
{
    private readonly ITourService _tourService;
   

    public TourController(ITourService tourService/*, ITourFilteringService filteringService*/)
    {
        _tourService = tourService;
        //_filteringService = filteringService;
    }
    
    [HttpGet]
    public ActionResult<PagedResult<TourDto>> GetPublished([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _tourService.GetPublishedPaged(page, pageSize);
        return CreateResponse(result);
    }

    /*[HttpGet]
    public ActionResult<PagedResult<TourDto>> GetFilteredTours([FromQuery] int page, [FromQuery] int pageSize, [FromBody] TourFilterCriteriaDto filter)
    {
        var result = _filteringService.GetFilteredTours(page, pageSize, filter);
        return CreateResponse(result);
    }
    */
}