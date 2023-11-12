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
    }
}
