using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterCompletionService : CrudService<EncounterCompletionDto, EncounterCompletion>, IEncounterCompletionService
    {
        protected IEncounterCompletionRepository _encoutnerCompletionRepository;

        public EncounterCompletionService(IEncounterCompletionRepository encoutnerCompletionRepository, IMapper mapper) : base(encoutnerCompletionRepository, mapper)
        {
            _encoutnerCompletionRepository = encoutnerCompletionRepository;
        }

        public Result<PagedResult<EncounterCompletionDto>> GetPagedByUser(int page, int pageSize, int userId)
        {
            var result = _encoutnerCompletionRepository.GetPagedByUser(page, page, userId);
            return MapToDto(result);
        }

        public EncounterCompletionDto GetByUserAndEncounter(int userId, int encounterId)
        {
            var result = _encoutnerCompletionRepository.GetByUserAndEncounter(userId, encounterId);
            return MapToDto(result);
        }
    }
}
