using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ShoppingCartRepository : CrudDatabaseRepository<ShoppingCart, ToursContext>, IShoppingCartRepository
    {
        private readonly DbSet<ShoppingCart> _dbSet;

        public ShoppingCartRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<ShoppingCart>();
        }

        public ShoppingCart GetByUser(int userId)
        {
            var shoppingCart = _dbSet.AsNoTracking().FirstOrDefault(tp => tp.UserId == userId);
            return shoppingCart;
        }
    }
}
