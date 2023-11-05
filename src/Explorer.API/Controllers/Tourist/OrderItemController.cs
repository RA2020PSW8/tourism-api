using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases.MarketPlace;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/orderItems")]
    public class OrderItemController: BaseApiController
    {
        private readonly IOrderItemService _orderItemService;


        public OrderItemController(IOrderItemService orderItemService)
        {

            _orderItemService = orderItemService;

        }


        [HttpGet]
        public ActionResult<PagedResult<OrderItemDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _orderItemService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }


        [HttpGet("byUser")]
        public ActionResult<PagedResult<OrderItemDto>> GetAllByUser([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _orderItemService.GetAllByUser(page, pageSize, ClaimsPrincipalExtensions.PersonId(User));
            var resultValue = Result.Ok(result);
            return CreateResponse(resultValue);
        }


        [HttpPost]
        public ActionResult<OrderItemDto> Create([FromBody] OrderItemDto orderItem)
        {
            var result = _orderItemService.Create(orderItem);
            return CreateResponse(result);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _orderItemService.Delete(id);
            return CreateResponse(result);
        }

    }
}
