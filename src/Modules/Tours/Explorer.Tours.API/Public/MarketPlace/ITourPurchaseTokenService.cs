using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.MarketPlace
{
    public interface ITourPurchaseTokenService
    {
        Result BuyShoppingCart(int shoppingCartId);
        Result<PagedResult<TourPurchaseTokenDto>> GetPaged(int page, int pageSize);
    }
}
