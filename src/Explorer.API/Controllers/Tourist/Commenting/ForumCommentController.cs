using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Commenting
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/commenting/comment")]
    public class ForumCommentController : BaseApiController
    {
        private readonly IForumCommentService _forumCommentService;

        public ForumCommentController(IForumCommentService forumCommentService)
        {
            _forumCommentService = forumCommentService;
        }

        [HttpGet]
        public ActionResult<ForumCommentDto> Get(int id)
        {
            var result = _forumCommentService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ForumCommentDto> Create([FromBody] ForumCommentDto comment)
        {
            var result = _forumCommentService.Create(comment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ForumCommentDto> Update([FromBody] ForumCommentDto comment)
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
