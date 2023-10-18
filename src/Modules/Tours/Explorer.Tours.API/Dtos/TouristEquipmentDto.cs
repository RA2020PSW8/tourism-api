using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TouristEquipmentDto
    {
        public long Id { get; set; }
        public long TouristId { get; init; }
        public long EquipmentId { get; init; }

        public TouristEquipmentDto(long id, long touristId, long equipmentId)
        {
            Id = id;
            TouristId = touristId;
            EquipmentId = equipmentId;
        }
    }
}
