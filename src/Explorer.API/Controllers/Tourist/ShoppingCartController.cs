using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shoppingCart")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _cartService;


        public ShoppingCartController(IShoppingCartService cartService)
        {

            _cartService = cartService;

        }
        [HttpGet("byUser")]
        public ActionResult<PagedResult<ShoppingCartDto>> GetByUser(int userId)
        {
            var result = _cartService.GetByUser(userId);
            var resultValue = Result.Ok(result);
            return CreateResponse(resultValue);
        }


        [HttpPost]
        public ActionResult<ShoppingCartDto> Create([FromBody] ShoppingCartDto club)
        {
            var result = _cartService.Create(club);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ShoppingCartDto> Update([FromBody] ShoppingCartDto club)
        {
            var result = _cartService.Update(club);
            return CreateResponse(result);
        }
    }
}
