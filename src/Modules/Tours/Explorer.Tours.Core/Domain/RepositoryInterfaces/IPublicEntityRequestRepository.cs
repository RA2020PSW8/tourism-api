using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Enums;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface IPublicEntityRequestRepository : ICrudRepository<PublicEntityRequest>
{
    PublicEntityRequest GetByEntityId(int entityId, EntityType entityType);
}