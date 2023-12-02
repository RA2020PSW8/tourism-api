using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "userPolicy")]
    [Route("api/encounter")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;

        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }


        [HttpGet]
        public ActionResult<PagedResult<EncounterDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EncounterDto> Create([FromBody] EncounterDto encounter)
        {
            encounter.UserId = User.PersonId();
            var result = _encounterService.Create(encounter);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EncounterDto> Update([FromBody] EncounterDto encounter)
        {
            var result = _encounterService.Update(encounter);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _encounterService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("status")]
        public ActionResult<PagedResult<EncounterDto>> GetByStatus([FromQuery] string status)
        {
            var result = _encounterService.GetAllByStatus(status);
            return CreateResponse(result);
        }
    }
}
