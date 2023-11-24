using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
{
    protected readonly IOrderItemRepository _orderItemRepository;
    protected readonly IShoppingCartRepository _shoppingCartRepository;
    protected readonly ITourPurchaseTokenRepository _tourPurchaseTokenRepository;

    public TourPurchaseTokenService(IOrderItemRepository orderItemRepository,
        IShoppingCartRepository shoppingCartRepository, ITourPurchaseTokenRepository repository,
        IMapper mapper) : base(repository, mapper)
    {
        _tourPurchaseTokenRepository = repository;
        _shoppingCartRepository = shoppingCartRepository;
        _orderItemRepository = orderItemRepository;
    }


    public Result BuyShoppingCart(int shoppingCartId)
    {
        ShoppingCart shoppingCart = GetShoppingCart(shoppingCartId);

        if(shoppingCart == null)
            return Result.Fail(FailureCode.NotFound).WithError("Shopping cart does not exist!");


        var tokens = new List<TourPurchaseToken>();

        foreach (var orderId in shoppingCart.OrdersId)
        {
            OrderItem orderItem = GetOrderItem(orderId);

            if (orderItem == null)
                return Result.Fail(FailureCode.NotFound).WithError("Order item does not exist!");


            if (TokenAlreadyExists(orderItem.TourId, shoppingCart.UserId))
                return Result.Fail(FailureCode.NotFound).WithError("Token already exists!");

            var token = new TourPurchaseToken(orderItem.TourId, shoppingCart.UserId);
            tokens.Add(token);
        }

        AddTokensToRepository(tokens);
        RemoveOrderItems(shoppingCart.OrdersId);
        DeleteShoppingCart(shoppingCartId);

        return Result.Ok();
    }

    private ShoppingCart GetShoppingCart(int shoppingCartId)
    {
        try
        {
            return _shoppingCartRepository.Get(shoppingCartId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private OrderItem GetOrderItem(int orderId)
    {
        try
        {
            return _orderItemRepository.Get(orderId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private bool TokenAlreadyExists(int tourId, int userId)
    {
        return _tourPurchaseTokenRepository.GetByTourAndTourist(tourId, userId) != null;
    }

    private void RemoveOrderItems(List<int> orderIds)
    {
        _orderItemRepository.RemoveRange(orderIds);
    }

    private void DeleteShoppingCart(int shoppingCartId)
    {
        _shoppingCartRepository.Delete(shoppingCartId);
    }

    private void AddTokensToRepository(List<TourPurchaseToken> tokens)
    {
        if (tokens.Count > 0)
        {
            _tourPurchaseTokenRepository.AddRange(tokens);
        }
    }
}