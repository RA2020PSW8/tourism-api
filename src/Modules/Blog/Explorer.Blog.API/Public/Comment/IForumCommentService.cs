using Explorer.Blog.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public.Comment
{
    public interface IForumCommentService
    {
        Result<ForumCommentDto> Get(int id);
        Result<ForumCommentDto> Create(ForumCommentDto forumCommentDto);
        Result<ForumCommentDto> Update(ForumCommentDto forumCommentDto);
        Result Delete(int id);
    }
}
