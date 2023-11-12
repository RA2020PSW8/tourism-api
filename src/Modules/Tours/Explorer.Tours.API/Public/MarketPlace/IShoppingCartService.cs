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
    public interface IShoppingCartService
    {
        Result<ShoppingCartDto> Create(ShoppingCartDto shoppingCart);
        Result<PagedResult<ShoppingCartDto>> GetPaged(int page, int pageSize);
        Result<ShoppingCartDto> Update(ShoppingCartDto shoppingCart);
        ShoppingCartDto GetByUser(int userId);
        Result Delete(int id);
    }
}
