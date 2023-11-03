using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface IKeypointRepository : ICrudRepository<Keypoint>
{
    public PagedResult<Keypoint> GetByTour(int page, int pageSize, int tourId);
}