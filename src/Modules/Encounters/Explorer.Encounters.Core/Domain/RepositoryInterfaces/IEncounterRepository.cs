
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Enums;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository : ICrudRepository<Encounter>
    {
        PagedResult<Encounter> GetAllByStatus(EncounterStatus status);
        IEnumerable<Encounter> GetAllByStatusAndType(EncounterStatus status, EncounterType type);
    }
}
