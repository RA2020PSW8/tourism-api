using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TouristEquipmentDto, TouristEquipment>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<KeypointDto, Keypoint>().ReverseMap();
        CreateMap<ObjectDto, Domain.Object>().ReverseMap();
        CreateMap<TourPreferenceDto, TourPreference>().ReverseMap();
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<TouristPositionDto, TouristPosition>().ReverseMap();
        CreateMap<PublicEntityRequestDto, PublicEntityRequest>().ReverseMap();
        CreateMap<PublicKeypointDto, PublicKeypoint>().ReverseMap();
        CreateMap<TourProgressDto, TourProgress>().ReverseMap();
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
        CreateMap<TourEquipmentDto, TourEquipment>().ReverseMap();
    }
}