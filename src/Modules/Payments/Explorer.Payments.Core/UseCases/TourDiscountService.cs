using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class TourDiscountService(ITourDiscountRepository tourDiscountRepository, IMapper mapper) : CrudService<TourDiscountDto, TourDiscount>(tourDiscountRepository, mapper), ITourDiscountService
{

    public new Result<TourDiscountDto> Create(TourDiscountDto tourDiscountDto)
    {
        var tour = MapToDomain(tourDiscountDto);
        var result = tourDiscountRepository.Create(tour);
        if (result == null)
            return Result.Fail("Tour is already on Discount");
        return MapToDto(tourDiscountRepository.Create(tour));
    }

    public new Result Delete(int tourId)
    {
        try
        {
            tourDiscountRepository.Delete(tourId);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail("Failed to delete the tour from the Discount.")
                .WithError(ex.Message);
        }
    }

    public Result<List<int>> GetToursFromOtherDiscount(int discountId)
    {
        return tourDiscountRepository.GetToursFromOtherDiscount(discountId);
    }
}