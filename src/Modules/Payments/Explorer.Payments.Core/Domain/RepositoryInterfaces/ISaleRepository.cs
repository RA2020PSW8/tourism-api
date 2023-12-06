using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ISaleRepository : ICrudRepository<Sale>
{
    public double GetDiscountForTour(int tourId);
    public IEnumerable<int> GetTourIdsSortedBySalePercentage();
    public PagedResult<Sale> GetSalesByAuthor(int userId, int page, int pageSize);
    public PagedResult<Sale> GetAllWithTours(int page, int pageSize);
}