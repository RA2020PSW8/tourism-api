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
    public interface IUserService
    {
        Result<PagedResult<UserDto>> GetPaged(int page, int pageSize);
        //Result<UserDto> Create(UserDto equipment);
        Result<UserDto> Update(UserDto user);
        //Result Delete(int id);
    }
}
