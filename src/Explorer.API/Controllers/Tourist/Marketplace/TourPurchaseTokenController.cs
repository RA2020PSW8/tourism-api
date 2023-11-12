using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

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
    }
}
