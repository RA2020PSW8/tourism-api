﻿using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database
{
    public class TourBundleRepository : CrudDatabaseRepository<TourBundle, PaymentsContext>, ITourBundleRepository
    {
        private readonly DbSet<TourBundle> _dbSet;

        public TourBundleRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<TourBundle>();
        }
    }
}
