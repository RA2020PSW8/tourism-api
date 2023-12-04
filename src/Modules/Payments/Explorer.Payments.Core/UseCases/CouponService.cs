﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class CouponService : CrudService<CouponDto, Coupon>, ICouponService
    {
        protected readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _couponRepository = repository;
        }

        private static string GenerateRandomAlphanumericString(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }
        public Result<CouponDto> Create(CouponDto coupon)
        {
            coupon.Code = GenerateRandomAlphanumericString();
            return base.Create(coupon);
        }

        public Result<PagedResult<CouponDto>> GetCouponForTourAndTourist(int page, int pageSize, int tourId, int touristId)
        {
            var result = _couponRepository.GetCouponForTourAndTourist(page, pageSize, tourId, touristId);
            return MapToDto(result);
        }

        public Result<PagedResult<CouponDto>> GetCouponForTouristAllTour(int page, int pageSize, int touristId)
        {
            var result = _couponRepository.GetCouponForTouristAllTour(page, pageSize, touristId);
            return MapToDto(result);
        }
    }
}