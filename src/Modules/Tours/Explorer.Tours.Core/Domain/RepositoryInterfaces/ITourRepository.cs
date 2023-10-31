using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourRepository : ICrudRepository<Tour>
{
    public PagedResult<Tour> GetPublishedPaged(int page, int pageSize);
}