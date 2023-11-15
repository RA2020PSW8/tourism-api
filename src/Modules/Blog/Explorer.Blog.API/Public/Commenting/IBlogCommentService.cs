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
        Result<PagedResult<BlogCommentDto>> GetPaged(int page,int pageSize,long blogId);
        Result<BlogCommentDto> Get(int id,long userId);
        Result<BlogCommentDto> Create(BlogCommentDto blogCommentDto);
        Result<BlogCommentDto> Update(BlogCommentDto blogCommentDto);
        Result Delete(int id);
    }
}
