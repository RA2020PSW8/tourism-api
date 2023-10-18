using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITouristEquipmentRepository
    {
        TouristEquipment GetByTouristAndEquipment(long touristId, long equipmentId);
        IEnumerable<TouristEquipment> GetAll();
    }
}

