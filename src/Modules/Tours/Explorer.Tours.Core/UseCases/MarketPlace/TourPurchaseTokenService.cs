﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.API.Public.TourAuthoring;
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
    public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
    {
        protected readonly ITourPurchaseTokenRepository _tourPurchaseTokenRepository;
        protected readonly IShoppingCartRepository _shoppingCartRepository;
        protected readonly IOrderItemRepository _orderItemRepository;

        public TourPurchaseTokenService(IOrderItemRepository orderItemRepository, IShoppingCartRepository shoppingCartRepository,ITourPurchaseTokenRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _tourPurchaseTokenRepository = repository;
            _shoppingCartRepository = shoppingCartRepository;
            _orderItemRepository = orderItemRepository;
        }


        public Result BuyShoppingCart(int shoppingCartId)
        {
            ShoppingCart shoppingCart = null;
            try
            {
                shoppingCart = _shoppingCartRepository.Get(shoppingCartId);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.NotFound).WithError("Shopping cart does not exist!");
            }

            List<TourPurchaseToken> tokens = new List<TourPurchaseToken>();
            foreach(int orderId in shoppingCart.OrdersId)
            {
                OrderItem orderItem = null;
                try
                {
                    orderItem = _orderItemRepository.Get(orderId);
                }
                catch (Exception ex)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Order item does not exist!");
                }

                if (_tourPurchaseTokenRepository.GetByTourAndTourist(orderItem.TourId, shoppingCart.UserId) != null)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Token already exists!");
                }

                TourPurchaseToken token = new TourPurchaseToken(orderItem.TourId, shoppingCart.UserId);
                tokens.Add(token);

            }

            if(tokens.Count > 0)
            {
                _tourPurchaseTokenRepository.AddRange(tokens);
            }

            _orderItemRepository.RemoveRange(shoppingCart.OrdersId);
            _shoppingCartRepository.Delete(shoppingCartId); 

            return Result.Ok();
        }
    }
}