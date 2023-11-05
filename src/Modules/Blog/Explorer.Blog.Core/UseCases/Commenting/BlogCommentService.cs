using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Commenting;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
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
        private readonly IUserService _userService;
        private readonly ICrudRepository<BlogComment> _repository;
        private readonly IMapper _mapper;

        public BlogCommentService(ICrudRepository<BlogComment> crudRepository,IMapper mapper, IUserRepository userRepository,IUserService userService) : base(mapper)
        {
            _repository = crudRepository;
            _userRepositoy = userRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public Result<BlogCommentDto> Create(BlogCommentDto blogCommentDto)
        {
            try
            {   
                var user = _userRepositoy.GetActiveById(blogCommentDto.UserId);
                var result = _repository.Create(MapToDomain(blogCommentDto));
                BlogCommentDto newDto = MapToDto(result);
                newDto.Username = user.Username;
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

        public Result<BlogCommentDto> Get(int id)
        {
            try
            {
                var result = _repository.Get(id);
                BlogCommentDto dto = MapToDto(result);
                
                //dto.Username = 
                return dto;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

        }

        public Result<PagedResult<BlogCommentDto>> GetPaged(int page, int pageSize)
        {
            var result = _repository.GetPaged(page, pageSize).Results;
            List<BlogCommentDto> dtos = new();
            foreach (var item in result)
            {
                dtos.Add(MapToDto(item));
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

        /* public void LoadUserInformation(BlogCommentDto dto, UserDto user)
         {
             if (user.Id == )
             {
                 dto.Username = user.Username;
             }
         }*/

        //public BlogCommentService(ICrudRepository<BlogComment> repository,IMapper mapper) : base(repository, mapper) { }
    }
}
