using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IClubChallengeRequestRepository : ICrudRepository<ClubChallengeRequest>
{
    ClubChallengeRequest GetByIdWithClubs(long id);
    ClubChallengeRequest GetNoTracking(long id);
}