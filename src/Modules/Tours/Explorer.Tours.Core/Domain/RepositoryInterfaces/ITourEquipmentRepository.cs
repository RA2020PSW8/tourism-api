using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourEquipmentRepository
    {
        List<Equipment> GetEquipmentForTour(int tourId);
        void AddEquipmentToTour(TourEquipment tourEquipment);
        void RemoveEquipmentFromTour(TourEquipment tourEquipment);

    }
}
