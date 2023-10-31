﻿using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PublicEntityRequest : Entity
    {
        public int UserId { get; init; }
        public int EntityId { get; init; }
        public EntityType EntityType { get; init; }
        public PublicEntityRequestStatus Status { get; init; }

        public PublicEntityRequest()
        {

        }

        public PublicEntityRequest(int userId, int entityId, EntityType entityType, PublicEntityRequestStatus status)
        {
            UserId = userId;
            EntityId = entityId;
            EntityType = entityType;
            Status = status;
            Validate();
        }

        private void Validate()
        {
            if (UserId <= 0) throw new ArgumentException("Invalid UserId");
            if (EntityId <= 0) throw new ArgumentException("Invalid entity(keypoint/object) id");
        }
    }
}
