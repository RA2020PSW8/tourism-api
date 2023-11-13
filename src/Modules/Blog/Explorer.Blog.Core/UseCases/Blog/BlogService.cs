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
using Explorer.Stakeholders.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Blog.Core.Domain.Enums;

namespace Explorer.Blog.Core.UseCases.Blog
{
    public class BlogService : BaseService<BlogDto,Domain.Blog>, IBlogService
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;
        public BlogService(IBlogRepository crudRepository, IMapper mapper, 
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
            try
            {
                _repository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<BlogDto> Get(int id)
        {
            try
            {
                var result = _repository.Get(id);
                BlogDto newDto = MapToDto(result);
                //TODO Promeni kada neko doda get({id})
                if(newDto.CreatorId != 0)
                {
                    var users = _userService.GetPaged(0, 0);
                    foreach (var user in users.Value.Results)
                    {
                        LoadUserInformation(newDto, user);
                    }

                    LoadPersonInformation(newDto);

                    foreach (var ratingDto in newDto.BlogRatings)
                    {
                       var user = _userService.GetPaged(0, 0).Value.Results.Find(u => u.Id == ratingDto.UserId);
                         if (user != null)
                          {
                              ratingDto.Username = user.Username;
                          }
                    }
                    
                }
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
                    LoadUserInformation(dto, user);
                }
            }

            foreach(BlogDto dto in dtos)
            {
                LoadPersonInformation(dto);
            }

            foreach (BlogDto dto in dtos) { 
                foreach(var ratingDto in dto.BlogRatings)
                {
                    var user = _userService.GetPaged(0,0).Value.Results.Find(u => u.Id == ratingDto.UserId);
                    if(user != null)
                    {
                        ratingDto.Username = user.Username;
                    }
                }
            }

            PagedResult<BlogDto> res = new(dtos, dtos.Count);
            return res;
        }

        public PagedResult<BlogDto> GetWithStatuses(int page, int pageSize)
        {
            var result = _repository.GetWithStatuses(page, pageSize).Results;
            List<BlogDto> dtos = new();
            foreach (var item in result)
            {
                dtos.Add(MapToDto(item));
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

        public void LoadPersonInformation(BlogDto dto)
        {
            if (dto.CreatorId != 0)
            {
                AccountRegistrationDto person = _profileService.GetProfile(dto.CreatorId).Value;
                dto.CreatorName = person.Name;
                dto.CreatorSurname = person.Surname;
            }
        }

        public void LoadUserInformation(BlogDto dto, UserDto user)
        {
            if (user.Id == dto.CreatorId && dto.CreatorId != 0)
            {
                dto.CreatorRole = user.Role;
            }
        }

        public void UpdateStatuses()
        {

        }
        //mislim da ovo treba da ima povratnu vrednost BlogRatingDto
        public Result<BlogDto> AddRating(BlogRatingDto blogRatingDto)
        {
            var blog = _repository.GetBlog(Convert.ToInt32(blogRatingDto.BlogId));
            var rating = new BlogRating(blogRatingDto.BlogId, blogRatingDto.UserId, blogRatingDto.CreationTime,Enum.Parse<Rating>(blogRatingDto.Rating));
            blog.AddRating(rating);
            
            return Update(MapToDto(blog));
        }
    }
}
