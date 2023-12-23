using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IClubRepository: ICrudRepository<Club>
{
    public Club GetUntracked(long id);
}