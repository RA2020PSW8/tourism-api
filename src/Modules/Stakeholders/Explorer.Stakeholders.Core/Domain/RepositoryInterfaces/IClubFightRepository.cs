using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IClubFightRepository : ICrudRepository<ClubFight>
{
    ClubFight GetCurrentFightForOneOfTwoClubs(long clubId1, long clubId2);
}