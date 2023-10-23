using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubJoinRequestRepository : ICrudRepository<ClubJoinRequest>
    {
        PagedResult<ClubJoinRequest> GetAllByUser(long userId);
        PagedResult<ClubJoinRequest> GetAllByClub(long clubId);
        bool Exists(long clubId, long userId);
    }
}
