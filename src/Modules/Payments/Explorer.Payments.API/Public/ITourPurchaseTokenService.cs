using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface ITourPurchaseTokenService
{
    Result BuyShoppingCart(int shoppingCartId);
    Result<PagedResult<TourPurchaseTokenDto>> GetPaged(int page, int pageSize);
}