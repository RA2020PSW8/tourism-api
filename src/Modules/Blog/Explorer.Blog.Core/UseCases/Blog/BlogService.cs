using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.Core.Domain;
using AutoMapper;
using Explorer.Blog.API.Public.Blog;
using FluentResults;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Blog.Core.UseCases.Blog
{
    public class BlogService : BaseService<BlogDto,Domain.Blog>, IBlogService
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly ICrudRepository<Domain.Blog> _repository;
        private readonly IMapper _mapper;
        public BlogService(ICrudRepository<Domain.Blog> crudRepository, IMapper mapper, 
            IUserService userService, IProfileService profileService) : base(mapper)
        {
            _repository = crudRepository;
            _mapper = mapper;
            _userService = userService;
            _profileService = profileService;
        }

        public Result<BlogDto> Create(BlogDto blog)
        {
            try
            {
                var result = _repository.Create(MapToDomain(blog));
                BlogDto newDto = MapToDto(result);
                var users = _userService.GetPaged(0, 0);
                foreach (var user in users.Value.Results)
                {
                    if (user.Id == newDto.CreatorId && user.Role == 0)
                        throw new ArgumentException("Administrator cannot create blog.");
                }
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Result<BlogDto> Get(int id)
        {
            try
            {
                var result = _repository.Get(id);
                BlogDto newDto = MapToDto(result);
                //TODO Promeni kada neko doda get({id})
                var users = _userService.GetPaged(0,0);
                foreach (var user in users.Value.Results)
                {
                    if(user.Id == newDto.CreatorId)
                    {
                        newDto.CreatorRole = user.Role;
                    }
                }

                AccountRegistrationDto person = _profileService.GetProfile(newDto.CreatorId).Value;
                newDto.CreatorName = person.Name;
                newDto.CreatorSurname = person.Surname;
                return newDto;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<BlogDto>> GetPaged(int page, int pageSize)
        {
            var result = _repository.GetPaged(page, pageSize).Results;
            List<BlogDto> dtos = new();
            foreach(var item in result)
            {
                dtos.Add(MapToDto(item));
            }
            //TODO Promeni kada neko doda get({id})
            var users = _userService.GetPaged(0, 0);
            foreach (var user in users.Value.Results)
            {
                foreach(BlogDto dto in dtos)
                {
                    if (user.Id == dto.CreatorId)
                    {
                        dto.CreatorRole = user.Role;
                    }
                }
            }

            foreach(BlogDto dto in dtos)
            {
                AccountRegistrationDto person = _profileService.GetProfile(dto.CreatorId).Value;
                dto.CreatorName = person.Name;
                dto.CreatorSurname = person.Surname;
            }

            PagedResult<BlogDto> res = new(dtos, dtos.Count);
            return res;
        }

        public Result<BlogDto> Update(BlogDto blog)
        {
            try
            {
                var result = _repository.Update(MapToDomain(blog));
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
