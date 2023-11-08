using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.TourExecution;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourExecution
{
    [Authorize(Policy = "touristAdminPolicy")]
    [Route("api/tourexecution/tourissue")]
    public class TourIssueController : BaseApiController
    {
        private readonly ITourIssueService _tourIssueService;

        public TourIssueController(ITourIssueService tourIssueService)
        {
            _tourIssueService = tourIssueService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<PagedResult<TourIssueDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize) 
        {
            var result = _tourIssueService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public ActionResult<TourIssueDto> Get(int id)
        {
            var result = _tourIssueService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourIssueDto> Create([FromBody] TourIssueDto issue) 
        {
            issue.Id = _tourIssueService.GetPaged(0, 0).Value.TotalCount + 1;
            issue.UserId = User.PersonId();
            var result = _tourIssueService.Create(issue);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourIssueDto> Update([FromBody] TourIssueDto issue) 
        {
            var result = _tourIssueService.Update(issue);
            return CreateResponse(result);
        }

        [HttpPut("resolve/{id:int}")]
        public ActionResult<TourIssueDto> Resolve([FromBody] TourIssueDto issue)
        {
            if (issue.UserId == User.PersonId())
            {
                var result = _tourIssueService.Update(issue);
                return CreateResponse(result);
            }
            else
                return null;
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

        [HttpGet("tour/{tourId:int}")]
        public ActionResult<PagedResult<TourIssueDto>> GetByTourId([FromQuery] int page, [FromQuery] int pageSize, int tourId)
        {
            var result = _tourIssueService.GetByTourId(page, pageSize, tourId);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("id/{tourIssueId:int}")]
        public ActionResult<PagedResult<TourIssueDto>> GetByTourIssueId([FromQuery] int page, [FromQuery] int pageSize, int tourIssueId)
        {
            var result = _tourIssueService.GetByTourIssueId(page, pageSize, tourIssueId);
            return CreateResponse(result);
        }
    }
}
