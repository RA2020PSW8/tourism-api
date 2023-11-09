using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IPublicEntityRequestRepository : ICrudRepository<PublicEntityRequest>
    {
        PublicEntityRequest GetByEntityId(int entityId, EntityType entityType);

    }
}
