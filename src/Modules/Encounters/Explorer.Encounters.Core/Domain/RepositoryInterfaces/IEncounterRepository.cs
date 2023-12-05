
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Enums;
using Explorer.Tours.Core.Domain;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository : ICrudRepository<Encounter>
    {
        PagedResult<Encounter> GetAllByStatus(EncounterStatus status);
        IEnumerable<Encounter> GetAllByStatusAndType(EncounterStatus status, EncounterType type);
        PagedResult<Encounter> GetNearbyByType(int page, int pageSize, double longitude, double latitude, EncounterType type);
    }
}
