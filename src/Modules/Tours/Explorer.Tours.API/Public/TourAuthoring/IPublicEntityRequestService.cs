using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Enums;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourAuthoring
{
    public interface IPublicEntityRequestService
    {
        public Result<PagedResult<PublicEntityRequestDto>> GetPaged(int page, int pageSize);
        public Result<PublicEntityRequestDto> Get(int id);
        public Result<PublicEntityRequestDto> Create(PublicEntityRequestDto publicEntityRequestDto);
        public Result Delete(int id);
        public Result<PublicEntityRequestDto> Approve(PublicEntityRequestDto publicEntityRequestDto);
        public Result<PublicEntityRequestDto> Decline(PublicEntityRequestDto publicEntityRequestDto);
        Result<PublicEntityRequestDto> GetByEntityId(int entityId, EntityType entityType);

    }
}
