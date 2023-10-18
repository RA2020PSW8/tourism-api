using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases.Tourist
{
    public class ClubJoinRequestService : CrudService<ClubJoinRequestDto, ClubJoinRequest>, IClubJoinRequestService
    {
        public ClubJoinRequestService(ICrudRepository<ClubJoinRequest> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<List<ClubJoinRequestDto>> GetAllByUserId(int id)
        {
            List<ClubJoinRequestDto> results = new List<ClubJoinRequestDto>();
            /*foreach (var keypoint in CrudRepository.GetPaged())
            {
                var result = CrudRepository.Create(MapToDomain(keypoint));
                results.Add(MapToDto(result));
            }*/

            return results;
        }
    }
}
