using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Commenting;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Commenting
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/blog/comments")]
    public class ForumCommentController : BaseApiController
    {
        private readonly IBlogCommentService _forumCommentService;

        public ForumCommentController(IBlogCommentService forumCommentService)
        {
            _forumCommentService = forumCommentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogCommentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _forumCommentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<BlogCommentDto> Get(int id)
        {
            var result = _forumCommentService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<BlogCommentDto> Create([FromBody] BlogCommentDto comment)
        {
            var result = _forumCommentService.Create(comment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<BlogCommentDto> Update([FromBody] BlogCommentDto comment)
        {
            var result = _forumCommentService.Update(comment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _forumCommentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
