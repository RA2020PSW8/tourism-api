using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.TourExecution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourExecution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourexecution/tourissue")]
    public class TourIssueController : BaseApiController
    {
        private readonly ITourIssueService _tourIssueService;

        public TourIssueController(ITourIssueService tourIssueService)
        {
            _tourIssueService = tourIssueService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourIssueDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize) 
        {
            var result = _tourIssueService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourIssueDto> Get(int id)
        {
            var result = _tourIssueService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourIssueDto> Create([FromBody] TourIssueDto issue) 
        {
            var result = _tourIssueService.Create(issue);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourIssueDto> Update([FromBody] TourIssueDto issue) 
        {
            var result = _tourIssueService.Update(issue);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            var result = _tourIssueService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("user/{userId:int}")]
        public ActionResult<PagedResult<TourIssueDto>> GetByUserAll([FromQuery] int page, [FromQuery] int pageSize, int userId)
        {
            var result = _tourIssueService.GetByUserPaged(page, pageSize, userId);
            return CreateResponse(result);
        }
    }
}
