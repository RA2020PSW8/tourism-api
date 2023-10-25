﻿using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TouristEquipment : Entity
    {
        public long TouristId { get; init; }
        public long EquipmentId { get; init; }

        public TouristEquipment(long touristId, long equipmentId)
        {
            TouristId = touristId;
            EquipmentId = equipmentId;
        }
    }

}
