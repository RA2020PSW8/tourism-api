﻿using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Explorer.Payments.API.Dtos;
using FluentResults;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class CouponRepository : CrudDatabaseRepository<Coupon, PaymentsContext>, ICouponRepository
    {
        protected readonly PaymentsContext _dbContext;
        private readonly DbSet<Coupon> _dbSet;

        public CouponRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Coupon>();
        }

        public Result<PagedResult<Coupon>> GetCouponForTourAndTourist(int page, int pageSize, int tourId, int touristId)
        {
            var task = _dbContext.Coupons.Where(c => c.TourId == tourId && c.TouristId == touristId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Result<PagedResult<Coupon>> GetCouponForTouristAllTour(int page, int pageSize, int touristId)
        {
            var task = _dbContext.Coupons.Where(c => c.TourId == 0 && c.TouristId == touristId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}