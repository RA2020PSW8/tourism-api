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
        Result<PersonDto> UpdateProfile(PersonDto updatedPerson);
        bool IsProfileAlreadyFollowed(long follower, long followed);
        Result<PersonDto> FollowProfile(long follower, long followed);
    }
}
