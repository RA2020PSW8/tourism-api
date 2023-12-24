using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Statistics
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/statistics")]
    public class StatisticsController : BaseApiController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
           _statisticsService = statisticsService;
        }

        [HttpGet("encounterCompletions")]
        public ActionResult<EncounterStatsDto> GetPagedByUser()
        {
            var userId = ClaimsPrincipalExtensions.PersonId(User);
            var result = _statisticsService.GetEncounterStatsByUser(userId);
            return CreateResponse(result);
        }

    }
}
