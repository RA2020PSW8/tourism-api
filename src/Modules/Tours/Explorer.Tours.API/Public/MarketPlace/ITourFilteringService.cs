using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.MarketPlace
{
    public interface ITourFilteringService
    {
        public Result<PagedResult<TourDto>> GetFilteredTours(int page, int pageSize, FilterCriteriaDto filter);
        //Result<PagedResult<PublicEntity>> GetPublicEntitites(int page, int pageSize, TourFilterCriteriaDto filter);
        //Result<SearchResultsDto> SearchMarketplace(int page, int pageSize, TourFilterCriteriaDto filter);
    }
}
