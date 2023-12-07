using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Marketplace;

[Route("api/marketplace/tour-sale")]
public class TourSaleController : BaseApiController
{
    private readonly ITourSaleService _saleService;

    public TourSaleController(ITourSaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpPost]
    public ActionResult<TourSaleDto> Create([FromBody] TourSaleDto saleDto)
    {
        var result = _saleService.Create(saleDto);
        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.Reasons });
        }
        return CreateResponse(result);
    }

    [HttpDelete("{tourId:int}")]
    public ActionResult Delete(int tourId)
    {
        var result = _saleService.Delete(tourId);
        return CreateResponse(result);
    }
}