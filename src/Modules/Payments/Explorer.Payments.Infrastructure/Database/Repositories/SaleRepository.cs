using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class SaleRepository: CrudDatabaseRepository<Sale, PaymentsContext>, ISaleRepository
{
    protected readonly PaymentsContext _dbContext;
    private readonly DbSet<Sale> _dbSet;

    public SaleRepository(PaymentsContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<Sale>();
    }

    public double GetDiscountForTour(int tourId)
    {
        var saleId = _dbContext.TourSales
            .Where(ts => ts.TourId == tourId)
            .Select(ts => ts.SaleId)
            .FirstOrDefault();

        if (saleId == 0) return 0;
        var sale = _dbContext.Sales.Find(saleId);
        if (sale != null)
            return sale.Percentage;

        return 0;
    }

    public IEnumerable<int> GetTourIdsSortedBySalePercentage()
    {
        // // Retrieve tour IDs sorted by sale percentage
        // var tourIds = _dbContext.TourSales
        //     .OrderByDescending(ts => ts.Sale.Percentage)
        //     .Select(ts => ts.TourId)
        //     .ToList();
        return null;
    }

    public PagedResult<Sale> GetSalesByAuthor(int userId, int page, int pageSize)
    {
        var task = _dbContext.Sales.Where(s => s.UserId == userId)
            .GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }

    public PagedResult<Sale> GetAllWithTours(int page, int pageSize)
    {
        var task = _dbSet.Include(s => s.TourSales).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }

}