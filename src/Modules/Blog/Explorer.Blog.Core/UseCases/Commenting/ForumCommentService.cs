using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Comment;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases.Commenting
{
    public class ForumCommentService : CrudService<ForumCommentDto,ForumComment>,IForumCommentService
    {
        public ForumCommentService(ICrudRepository<ForumComment> repository,IMapper mapper) : base(repository, mapper) { }

        public Result<ForumCommentDto> Get(int id)
        {
            try
            {
                var result = CrudRepository.Get(id);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public override Result<ForumCommentDto> Create(ForumCommentDto forumCommentDto)
        {
            try
            {
                var result = CrudRepository.Create(MapToDomain(forumCommentDto));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public override Result<ForumCommentDto> Update(ForumCommentDto forumCommentDto)
        {
            try
            {
                var result = CrudRepository.Update(MapToDomain(forumCommentDto));
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public override Result Delete(int id)
        {
            try
            {
                CrudRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
