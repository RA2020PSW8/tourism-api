using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourProgressRepository : ICrudRepository<TourProgress>
{
    TourProgress GetActiveByUser(long userId);

    List<TourProgress> GetCompletedByUser(long userId);
    List<TourProgress> GetActiveTours();
    public TourProgress GetByUser(long userId);
}