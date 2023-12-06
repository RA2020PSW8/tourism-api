using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface ISaleService
{
    Result<SaleDto> Create(SaleDto sale);
    Result<SaleDto> Update(SaleDto sale);
    Result Delete(int id);
    Result<double> GetDiscountForTour(int tourId);
    Result<IEnumerable<int>> GetTourIdsSortedBySalePercentage();
    Result<PagedResult<SaleDto>> GetSalesByAuthor(int userId, int page, int pageSize);
    Result<PagedResult<SaleDto>> GetAllWithTours(int page, int pageSize);
}