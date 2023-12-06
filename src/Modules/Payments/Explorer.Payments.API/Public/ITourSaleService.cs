using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface ITourSaleService
{
    Result<TourSaleDto> Create(TourSaleDto tourSaleDto);
    Result Delete(int tourId);
}