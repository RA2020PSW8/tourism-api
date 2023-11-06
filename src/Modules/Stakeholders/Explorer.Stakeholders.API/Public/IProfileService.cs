using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IProfileService
    {
        Result<AccountRegistrationDto> GetProfile(long userId);
        Result<PagedResult<PersonDto>> GetFollowers(long userId);
        Result<PagedResult<PersonDto>> GetFollowing(long userId);
        Result<PagedResult<PersonDto>> GetUserNonFollowedProfiles(int page, int pageSize, long userId);
        Result<PersonDto> UpdateProfile(PersonDto updatedPerson);
        Result<PagedResult<PersonDto>> Follow(long followerId, PersonDto followed);
        Result<PagedResult<PersonDto>> Unfollow(long followerId, PersonDto unfollowed);
    }
}
