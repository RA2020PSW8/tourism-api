using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class KeypointProfile : Profile
{
    public KeypointProfile()
    {
        CreateMap<KeypointDto, Keypoint>().ReverseMap();
    }
}