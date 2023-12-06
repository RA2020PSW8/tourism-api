using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class TourSaleRepository: CrudDatabaseRepository<TourSale, PaymentsContext>, ITourSaleRepository
{
    private readonly PaymentsContext _dbContext;
    private readonly DbSet<TourSale> _dbSet;

    public TourSaleRepository(PaymentsContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TourSale>();
    }

    public new TourSale Create(TourSale tourSale)
    {
        var existingTourSale = _dbContext.TourSales.FirstOrDefault(ts => ts.TourId == tourSale.TourId);

        if (existingTourSale == null)
        {
            _dbContext.TourSales.Add(tourSale);
            _dbContext.SaveChanges();
            return tourSale;
        }

        return null;
    }

    public void Delete(int tourId)
    {
        var tourSaleToRemove = _dbSet.SingleOrDefault(ts => ts.TourId == tourId);

        if (tourSaleToRemove == null) return;
        _dbSet.Remove(tourSaleToRemove);
        _dbContext.SaveChanges();
    }
}