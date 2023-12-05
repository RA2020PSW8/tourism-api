using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Infrastructure.Database.Repositories;
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
        protected readonly ITourRepository _tourRepository;

        public CouponService(ICouponRepository repository, IMapper mapper, ITourRepository tourRepository) : base(repository, mapper)
        {
            _couponRepository = repository;
            _tourRepository = tourRepository;
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
            Tour tour = _tourRepository.Get(tourId);
            var result = _couponRepository.GetCouponForTourAndTourist(page, pageSize, tourId, touristId);
            int authorId = tour.UserId;
            var resultAuthor = _couponRepository.GetCouponForAuthorAndTourist(page, pageSize, authorId, touristId);

            PagedResult<Coupon> couponsFromTour = result.Value;
            PagedResult<Coupon> couponsFromAuthor = resultAuthor.Value;

            List<Coupon> allCouponsList = new List<Coupon>(couponsFromTour.Results);
            allCouponsList.AddRange(couponsFromAuthor.Results);

            PagedResult<Coupon> allCoupons = new PagedResult<Coupon>(allCouponsList, allCouponsList.Count);

            return MapToDto(allCoupons);
        }

        public Result<PagedResult<CouponDto>> GetCouponsForAuthor(int page, int pageSize, int authorId)
        {
            var result = _couponRepository.GetCouponsForAuthor(page, pageSize, authorId);
            return MapToDto(result);
        }
    }
}
