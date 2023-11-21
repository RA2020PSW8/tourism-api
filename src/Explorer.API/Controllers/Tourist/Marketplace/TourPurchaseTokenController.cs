using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.Core.UseCases.TourAuthoring;

namespace Explorer.API.Controllers.Tourist.Marketplace
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/marketplace/tours/token")]
    public class TourPurchaseTokenController : BaseApiController
    {
        private readonly ITourPurchaseTokenService _tourPurchaseTokenService;

        public TourPurchaseTokenController(ITourPurchaseTokenService tourPurchaseTokenService)
        {
            _tourPurchaseTokenService = tourPurchaseTokenService;
        }

        [HttpPut("{id:int}")]
        public ActionResult BuyShoppingCart(int id)
        {
            var result = _tourPurchaseTokenService.BuyShoppingCart(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<TourPurchaseTokenDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourPurchaseTokenService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}
