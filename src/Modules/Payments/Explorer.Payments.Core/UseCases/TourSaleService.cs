using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class TourSaleService:CrudService<TourSaleDto, TourSale>, ITourSaleService
{
    private readonly ITourSaleRepository _saleRepository;

    public TourSaleService(ITourSaleRepository tourSaleRepository, IMapper mapper) : base(tourSaleRepository, mapper)
    {
        _saleRepository = tourSaleRepository;
    }

    public new Result<TourSaleDto> Create(TourSaleDto tourSaleDto)
    {
        var tour = MapToDomain(tourSaleDto);
        var result = _saleRepository.Create(tour);
        if (result == null)
            return Result.Fail("Tour is alredy on sale");
        return MapToDto(_saleRepository.Create(tour));
    }

    public new Result Delete(int tourId)
    {
        try
        {
            _saleRepository.Delete(tourId);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            // Handle exceptions, log them, etc.
            return Result.Fail("Failed to delete the tour from the sale.")
                .WithError(ex.Message);
        }
    }
}