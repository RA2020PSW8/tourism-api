using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService: CrudService<EncounterDto, Encounter>, IEncounterService
    {
        protected IEncounterRepository _encounterRepository;

        public EncounterService(IEncounterRepository encounterRepository, IMapper mapper): base(encounterRepository, mapper)
        {
            _encounterRepository = encounterRepository;
        }
    }
}
