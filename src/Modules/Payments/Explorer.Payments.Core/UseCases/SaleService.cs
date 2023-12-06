using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class SaleService:CrudService<SaleDto, Sale>, ISaleService
{
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository, IMapper mapper) : base(saleRepository, mapper)
    {
        _saleRepository = saleRepository;
    }

    public Result<double> GetDiscountForTour(int tourId)
    {
        return _saleRepository.GetDiscountForTour(tourId);
    }

    public Result<IEnumerable<int>> GetTourIdsSortedBySalePercentage()
    {
        return _saleRepository.GetTourIdsSortedBySalePercentage().ToList();
    }

    public Result<PagedResult<SaleDto>> GetSalesByAuthor(int userId, int page, int pageSize)
    {
        var result = _saleRepository.GetSalesByAuthor(userId, page, pageSize);
        return MapToDto(result);
    }

    public Result<PagedResult<SaleDto>> GetAllWithTours(int page, int pageSize)
    {
        var result = _saleRepository.GetAllWithTours(page, pageSize);
        return MapToDto(result);
    }

}