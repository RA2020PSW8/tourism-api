using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers
{
    public class TourEquipmentProfile: Profile
    {
        public TourEquipmentProfile()
        {
            CreateMap<TourEquipmentDto, TourEquipment>().ReverseMap();
        }
    }
}
