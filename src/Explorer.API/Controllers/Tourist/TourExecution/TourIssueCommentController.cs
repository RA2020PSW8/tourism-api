using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.TourExecution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourExecution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourexecution/tourissuecomment")]
    public class TourIssueCommentController : BaseApiController
    {
        private readonly ITourIssueCommentService _tourIssueCommentService;

        public TourIssueCommentController(ITourIssueCommentService tourIssueCommentService)
        {
            _tourIssueCommentService = tourIssueCommentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourIssueCommentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourIssueCommentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourIssueCommentDto> Get(int id)
        {
            var result = _tourIssueCommentService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourIssueCommentDto> Create([FromBody] TourIssueCommentDto comment)
        {
            var result = _tourIssueCommentService.Create(comment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourIssueCommentDto> Update([FromBody] TourIssueCommentDto comment)
        {
            var result = _tourIssueCommentService.Update(comment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourIssueCommentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
