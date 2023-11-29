using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
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

    public TourController(ITourService tourService)
    {
        _tourService = tourService;
    }
    
    [HttpGet]
    public ActionResult<PagedResult<TourDto>> GetPublished([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _tourService.GetPublishedPaged(page, pageSize);
        return CreateResponse(result);
    }

    [HttpGet("arhived-published")]
    [AllowAnonymous]
    public ActionResult<PagedResult<TourDto>> GetArchivedAndPublishedPaged([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _tourService.GetArchivedAndPublishedPaged(page, pageSize);
        return CreateResponse(result);
    }
    
    [HttpGet("custom")]
    public ActionResult<PagedResult<TourDto>> GetCustomByUser([FromQuery] int page, [FromQuery] int pageSize)
    {
        var touristId = ClaimsPrincipalExtensions.PersonId(User);
        var result = _tourService.GetCustomByUserPaged(touristId, page, pageSize);
        return CreateResponse(result);
    }

}