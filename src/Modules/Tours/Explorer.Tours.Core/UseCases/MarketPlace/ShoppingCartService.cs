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
        private readonly ICrudRepository<ShoppingCart> _cartRepository;
        protected readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartService(ICrudRepository<ShoppingCart> repository, IMapper mapper, IShoppingCartRepository rep) : base(repository, mapper) {
            _cartRepository = repository;
            _shoppingCartRepository = rep;
        }

        override public Result<ShoppingCartDto> Update(ShoppingCartDto updatedShoppingCart)
        {
            try
            {
                var existingShoppingCart = _cartRepository.Get(updatedShoppingCart.UserId);

                if (existingShoppingCart == null)
                {
                    return Result.Fail("Shopping cart not found.");
                }

                existingShoppingCart.OrdersId = updatedShoppingCart.OrdersId;
                existingShoppingCart.Price = updatedShoppingCart.Price;
                _cartRepository.Update(existingShoppingCart);
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
            var result = CrudRepository.Create(MapToDomain(entity));
            return MapToDto(result);
        }
        public ShoppingCartDto GetByUser(int userId)
        {
            var cart = _shoppingCartRepository.GetByUser(userId);
            return MapToDto(cart);
        }


    }
}
