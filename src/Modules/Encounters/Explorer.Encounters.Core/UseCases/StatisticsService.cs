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
    public class StatisticsService : IStatisticsService
    {
        protected IEncounterCompletionRepository _encounterCompletionRepository;

        public StatisticsService(IEncounterCompletionRepository encoutnerCompletionRepository, IMapper mapper)
        { 
            _encounterCompletionRepository = encoutnerCompletionRepository;
        }

        public Result<EncounterStatsDto> GetEncounterStatsByUser(int userId) 
        { 
            EncounterStatsDto encounterStats = new EncounterStatsDto();
            encounterStats.CompletedCount = _encounterCompletionRepository.GetCompletedCountByUser(userId);
            encounterStats.FailedCount = _encounterCompletionRepository.GetFailedCountByUser(userId);
            return encounterStats;
        }
    }
}
