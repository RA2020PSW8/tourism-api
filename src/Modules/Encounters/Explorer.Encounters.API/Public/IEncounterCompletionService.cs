﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterCompletionService
    {
        Result<PagedResult<EncounterCompletionDto>> GetPaged(int page, int pageSize);
        Result<PagedResult<EncounterCompletionDto>> GetPagedByUser(int page, int pageSize, int userId);
        EncounterCompletionDto GetByUserAndEncounter(int userId, int encounterId);
        void UpdateSocialEncounters();
    }
}
