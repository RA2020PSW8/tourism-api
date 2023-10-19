using AutoMapper;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class ObjectProfile : Profile
    {
        public ObjectProfile()
        {
            CreateMap<ObjectDto, Domain.Object>().ReverseMap();
        }
    }
}
