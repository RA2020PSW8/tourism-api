using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public.Commenting
{
    public interface IForumCommentService
    {
        Result<PagedResult<ForumCommentDto>> GetPaged(int page,int pageSize);
        Result<ForumCommentDto> Get(int id);
        Result<ForumCommentDto> Create(ForumCommentDto forumCommentDto);
        Result<ForumCommentDto> Update(ForumCommentDto forumCommentDto);
        Result Delete(int id);
    }
}
