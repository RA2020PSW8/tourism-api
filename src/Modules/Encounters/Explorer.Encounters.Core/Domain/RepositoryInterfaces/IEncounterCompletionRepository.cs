﻿using Explorer.BuildingBlocks.Core.UseCases;
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
    }
}
