using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Marketplace;

[Route("api/marketplace/sales")]
public class SaleController:BaseApiController
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public ActionResult<PagedResult<SaleDto>> GetAll(int page, int pageSize)
    {
        var result = _saleService.GetAllWithTours(page, pageSize);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<SaleDto> Create([FromBody] SaleDto saleDto)
    {
        var result = _saleService.Create(saleDto);
        return CreateResponse(result);
    }

    [HttpDelete("{tourId:int}")]
    public ActionResult Delete(int tourId)
    {
        var result = _saleService.Delete(tourId);
        return CreateResponse(result);
    }

    [HttpGet("sorted")]
    public ActionResult<IEnumerable<int>> GetTourIdsSortedBySalePercentage()
    {
        var result = _saleService.GetTourIdsSortedBySalePercentage();
        return CreateResponse(result);
    }

    [HttpGet("author-sales")]
    public ActionResult<PagedResult<SaleDto>> GetSalesByAuthor([FromQuery] int page, [FromQuery] int pageSize)
    {
        var authorId = ClaimsPrincipalExtensions.PersonId(User);
        var result = _saleService.GetSalesByAuthor(authorId, page, pageSize);
        return CreateResponse(result);
    }
}