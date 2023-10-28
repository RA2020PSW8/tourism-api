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
    public interface IBlogCommentService
    {
        Result<PagedResult<BlogCommentDto>> GetPaged(int page,int pageSize);
        Result<BlogCommentDto> Get(int id);
        Result<BlogCommentDto> Create(BlogCommentDto forumCommentDto);
        Result<BlogCommentDto> Update(BlogCommentDto forumCommentDto);
        Result Delete(int id);
    }
}
