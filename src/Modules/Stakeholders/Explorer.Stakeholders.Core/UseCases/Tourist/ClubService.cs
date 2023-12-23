using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases.Tourist;

public class ClubService : CrudService<ClubDto, Club>, IClubService
{
    private IClubRepository _clubRepository;
    public ClubService(IClubRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _clubRepository = repository;
    }

    public Result<ClubDto> GetUntracked(long id)
    {
        var result =  _clubRepository.GetUntracked(id);
        return MapToDto(result);
    }

    public PagedResult<ClubDto> GetAllByUser(int page, int pageSize, int currentUserId)
    {
        // var result = GetPaged(page, pageSize);
        // var filteredItems = new List<ClubDto>();
        //
        // foreach (var c in result.ValueOrDefault.Results)
        //     if (c.OwnerId == currentUserId)
        //         filteredItems.Add(c);
        //
        // return new PagedResult<ClubDto>(filteredItems, filteredItems.Count);
        throw new NotImplementedException();
    }
}