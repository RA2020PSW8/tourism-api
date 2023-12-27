using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterCompletionRepository : ICrudRepository<EncounterCompletion>
    {
        PagedResult<EncounterCompletion> GetPagedByUser(int page, int pageSize, long userId);
        EncounterCompletion GetByUserAndEncounter(long userId, long encounterId);
        EncounterCompletion GetByEncounter(long encounterId);
        bool HasUserStartedEncounter(long userId, long encounterId);
        bool HasUserCompletedEncounter(long userId, long encounterId);
        int GetCompletedCountByUser(long userId);
        int GetFailedCountByUser(long userId);
        int GetCompletedCountByUserAndMonth(long userId, int monthIndex, int year);
        int GetFailedCountByUserAndMonth(long userId, int monthIndex, int year);
 
    }
}
