using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourRepository : ICrudRepository<Tour>
{
    public PagedResult<Tour> GetByAuthorPaged(int page, int pageSize, int authorId);
    public PagedResult<Tour> GetPublishedPaged(int page, int pageSize);
    public Tour GetById(int id);
}