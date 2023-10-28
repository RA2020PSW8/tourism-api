using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class TourEquipmentProfile : Profile
    {
        public TourEquipmentProfile()
        {
            CreateMap<TourEquipmentDto, TourEquipment>().ReverseMap();
        }
    }
}
