using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Tour
{
    public interface ITourEquipmentService
    {
        Result<List<EquipmentDto>> GetEquipmentForTour(int tourId);
        Result AddEquipmentToTour(TourEquipmentDto tourEquipmentDto);
        Result RemoveEquipmentFromTour(TourEquipmentDto tourEquipmentDto);
    }
}
