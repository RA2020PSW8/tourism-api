using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Commenting;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases.Commenting
{
    public class BlogCommentService : BaseService<BlogCommentDto,BlogComment>,IBlogCommentService
    {
        private readonly IUserRepository _userRepositoy;
        private readonly ICrudRepository<BlogComment> _repository;
        private readonly IMapper _mapper;

        public BlogCommentService(ICrudRepository<BlogComment> crudRepository,IMapper mapper, IUserRepository userRepository) : base(mapper)
        {
            _repository = crudRepository;
            _userRepositoy = userRepository;
            _mapper = mapper;
        }

        public Result<BlogCommentDto> Create(BlogCommentDto blogCommentDto)
        {
            try
            {   
                var result = _repository.Create(MapToDomain(blogCommentDto));
                BlogCommentDto newDto = MapToDto(result);
                return newDto;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Result.Ok();
            }
            catch(ArgumentException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<BlogCommentDto> Get(int id,long userId)
        {
            try
            {
                var result = _repository.Get(id);
                BlogCommentDto dto = MapToDto(result);
                var user = _userRepositoy.GetActiveById(userId);
                dto.Username = user.Username;
                return dto;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<BlogCommentDto>> GetPaged(int page, int pageSize,long blogId)
        {
            var result = _repository.GetPaged(page, pageSize).Results;
            List<BlogCommentDto> dtos = new();
            foreach (var item in result)
            {
                if (item.BlogId == blogId)
                {
                    dtos.Add(MapToDto(item));
                }
            }

            foreach(var dto in dtos)
            {
                var user = _userRepositoy.GetActiveById(dto.UserId);
                dto.Username = user.Username;
            }

            PagedResult<BlogCommentDto> finalResult = new(dtos, dtos.Count);
            return finalResult;
        }

        public Result<BlogCommentDto> Update(BlogCommentDto blogCommentDto)
        {
            try
            {
                var result = _repository.Update(MapToDomain(blogCommentDto));
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
    }
}
