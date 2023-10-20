using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases.Tourist
{
    public class ClubService: CrudService<ClubDto, Club>, IClubService
    {
        public ClubService(ICrudRepository<Club> repository, IMapper mapper) : base(repository, mapper) { }

    }
}
