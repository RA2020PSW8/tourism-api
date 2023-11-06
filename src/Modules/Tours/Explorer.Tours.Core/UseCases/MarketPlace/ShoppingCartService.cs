using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.MarketPlace
{
    public class ShoppingCartService: CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {
        protected readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartService(IShoppingCartRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _shoppingCartRepository = repository;
        }


        override public Result<ShoppingCartDto> Update(ShoppingCartDto updatedShoppingCart)
        {
            try
            {
                var existingShoppingCart = _shoppingCartRepository.Get(updatedShoppingCart.UserId);

                if (existingShoppingCart == null)
                {
                    return Result.Fail("Shopping cart not found.");
                }

                existingShoppingCart.OrdersId = updatedShoppingCart.OrdersId;
                existingShoppingCart.Price = updatedShoppingCart.Price;
                _shoppingCartRepository.Update(existingShoppingCart);
                return Result.Ok(new ShoppingCartDto
                {
                    UserId = existingShoppingCart.UserId,
                    OrdersId = existingShoppingCart.OrdersId
                });
            }
            catch (Exception ex)
            {
                return Result.Fail($"An error occurred while updating the shopping cart: {ex.Message}");
            }
        }

        override public Result<ShoppingCartDto> Create(ShoppingCartDto entity)
        {
            var result = _shoppingCartRepository.Create(MapToDomain(entity));
            return MapToDto(result);
        }
        public ShoppingCartDto GetByUser(int userId)
        {
            var cart = _shoppingCartRepository.GetByUser(userId);
            return MapToDto(cart);
        }


    }
}
