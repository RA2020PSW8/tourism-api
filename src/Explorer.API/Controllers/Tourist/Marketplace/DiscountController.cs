using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Explorer.API.Controllers.Tourist.Marketplace;

[Route("api/marketplace/discounts")]
public class DiscountController(IDiscountService discountService) : BaseApiController
{

    [HttpGet]
    public ActionResult<PagedResult<DiscountDto>> GetAll(int page, int pageSize)
    {
        var result = discountService.GetAllWithTours(page, pageSize);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<DiscountDto> Create([FromBody] DiscountDto discountDto)
    {
        discountDto.UserId = ClaimsPrincipalExtensions.PersonId(User);
        var result = discountService.Create(discountDto);
        return CreateResponse(result);
    }

    [HttpPut]
    public ActionResult<DiscountDto> Update([FromBody] DiscountDto discountDto)
    {
        var result = discountService.Update(discountDto);
        return CreateResponse(result);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var result = discountService.Delete(id);
        return CreateResponse(result);
    }

    [HttpGet("author-discounts")]
    public ActionResult<PagedResult<DiscountDto>> GetDiscountsByAuthor([FromQuery] int page, [FromQuery] int pageSize)
    {
        var authorId = ClaimsPrincipalExtensions.PersonId(User);
        var result = discountService.GetDiscountsByAuthor(authorId, page, pageSize);
        return CreateResponse(result);
    }
}