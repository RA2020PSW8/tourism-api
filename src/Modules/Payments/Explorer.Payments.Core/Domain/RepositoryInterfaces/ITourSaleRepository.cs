using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ITourSaleRepository : ICrudRepository<TourSale>
{
    public void Delete(int tourId);
}