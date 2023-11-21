using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public.Blog;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.Enums;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using FluentResults;

namespace Explorer.Blog.Core.UseCases.Blog;

public class BlogService : BaseService<BlogDto, Domain.Blog>, IBlogService
{
    private readonly IBlogStatusService _blogStatusService;
    private readonly IMapper _mapper;
    private readonly IProfileService _profileService;
    private readonly IBlogRepository _repository;
    private readonly IUserService _userService;

    public BlogService(IBlogStatusService blogStatusService, IBlogRepository crudRepository, IMapper mapper,
        IUserService userService, IProfileService profileService) : base(mapper)
    {
        _blogStatusService = blogStatusService;
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
            var newDto = MapToDto(result);
            var users = _userService.GetPaged(0, 0);
            foreach (var user in users.Value.Results)
                if (user.Id == newDto.CreatorId && user.Role == 0)
                    throw new ArgumentException("Administrator cannot create blog.");
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
            var newDto = MapToDto(result);
            //TODO Promeni kada neko doda get({id})
            if (newDto.CreatorId != 0)
            {
                var users = _userService.GetPaged(0, 0);
                foreach (var user in users.Value.Results) LoadUserInformation(newDto, user);

                LoadPersonInformation(newDto);

                foreach (var ratingDto in newDto.BlogRatings)
                {
                    var user = _userService.GetPaged(0, 0).Value.Results.Find(u => u.Id == ratingDto.UserId);
                    if (user != null) ratingDto.Username = user.Username;
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
        foreach (var item in result) dtos.Add(MapToDto(item));

        //TODO Promeni kada neko doda get({id})
        var users = _userService.GetPaged(0, 0);
        foreach (var user in users.Value.Results)
        foreach (var dto in dtos)
            LoadUserInformation(dto, user);

        foreach (var dto in dtos) LoadPersonInformation(dto);

        foreach (var dto in dtos)
        foreach (var ratingDto in dto.BlogRatings)
        {
            var user = _userService.GetPaged(0, 0).Value.Results.Find(u => u.Id == ratingDto.UserId);
            if (user != null) ratingDto.Username = user.Username;
        }

        PagedResult<BlogDto> res = new(dtos, dtos.Count);
        return res;
    }

    public PagedResult<BlogDto> GetWithStatuses(int page, int pageSize)
    {
        var result = _repository.GetWithStatuses(page, pageSize).Results;
        List<BlogDto> dtos = new();
        foreach (var item in result) dtos.Add(MapToDto(item));
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

    public void UpdateStatuses()
    {
        var result = _repository.GetPaged(0, 0).Results;

        foreach (var blog in result)
        {
            var upvotes = blog.BlogRatings.Count(b => b.Rating == Rating.UPVOTE);
            var downvotes = blog.BlogRatings.Count(b => b.Rating == Rating.DOWNVOTE);
            if (upvotes - downvotes < 0)
                blog.CloseBlog();
            else
                _blogStatusService.Generate(blog.Id, "POPULAR");
        }
    }

    public Result<BlogDto> AddRating(BlogRatingDto blogRatingDto, long userId)
    {
        var blog = _repository.GetBlog(Convert.ToInt32(blogRatingDto.BlogId));
        var rating = new BlogRating(blogRatingDto.BlogId, userId, blogRatingDto.CreationTime,
            Enum.Parse<Rating>(blogRatingDto.Rating));
        blog.AddRating(rating);
        //UpdateStatuses();
        return Update(MapToDto(blog));
    }

    public void LoadPersonInformation(BlogDto dto)
    {
        if (dto.CreatorId != 0)
        {
            var person = _profileService.GetProfile(dto.CreatorId).Value;
            dto.CreatorName = person.Name;
            dto.CreatorSurname = person.Surname;
        }
    }

    public void LoadUserInformation(BlogDto dto, UserDto user)
    {
        if (user.Id == dto.CreatorId && dto.CreatorId != 0) dto.CreatorRole = user.Role;
    }
}