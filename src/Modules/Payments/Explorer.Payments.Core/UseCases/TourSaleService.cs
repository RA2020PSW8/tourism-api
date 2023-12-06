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
}