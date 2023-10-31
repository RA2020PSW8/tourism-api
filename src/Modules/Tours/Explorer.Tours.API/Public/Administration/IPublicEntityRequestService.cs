using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IPublicEntityRequestService
    {
        public Result<PagedResult<PublicEntityRequestDto>> GetPaged(int page , int pageSize);
        public Result<PublicEntityRequestDto> Get(int id);
        public Result<PublicEntityRequestDto> Create(PublicEntityRequestDto entity);
        public Result<PublicEntityRequestDto> Update(PublicEntityRequestDto entity);
        public Result Delete(int id);
    }
}
