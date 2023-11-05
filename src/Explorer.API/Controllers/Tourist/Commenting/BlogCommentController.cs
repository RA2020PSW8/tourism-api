using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Commenting;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Commenting
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/blog/comments")]
    public class BlogCommentController : BaseApiController
    {
        private readonly IBlogCommentService _blogCommentService;

        public BlogCommentController(IBlogCommentService blogCommentService)
        {
            _blogCommentService = blogCommentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogCommentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogCommentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<BlogCommentDto> Get(int id)
        {
            var result = _blogCommentService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<BlogCommentDto> Create([FromBody] BlogCommentDto comment)
        {
            comment.UserId = User.PersonId();
            var result = _blogCommentService.Create(comment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<BlogCommentDto> Update([FromBody] BlogCommentDto comment)
        {
            var result = _blogCommentService.Update(comment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _blogCommentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
