using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;
using AutoMapper;
namespace Explorer.Payments.Core.Mappers;

public class PaymentsProfile: Profile
{
    public PaymentsProfile()
    {
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
    }
}