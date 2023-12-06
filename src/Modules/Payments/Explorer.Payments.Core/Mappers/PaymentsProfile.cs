using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;

namespace Explorer.Payments.Core.Mappers;

public class PaymentsProfile : Profile
{
    public PaymentsProfile()
    {
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
        CreateMap<WalletDto, Wallet>().ReverseMap();
        CreateMap<SaleDto, Sale>().ReverseMap();
        CreateMap<TourSaleDto, TourSale>().ReverseMap();
    }
}