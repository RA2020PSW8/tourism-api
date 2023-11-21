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
        ShoppingCart shoppingCart = null;
        try
        {
            shoppingCart = _shoppingCartRepository.Get(shoppingCartId);
        }
        catch (Exception ex)
        {
            return Result.Fail(FailureCode.NotFound).WithError("Shopping cart does not exist!");
        }

        var tokens = new List<TourPurchaseToken>();
        foreach (var orderId in shoppingCart.OrdersId)
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
                return Result.Fail(FailureCode.NotFound).WithError("Token already exists!");

            var token = new TourPurchaseToken(orderItem.TourId, shoppingCart.UserId);
            tokens.Add(token);
        }

        if (tokens.Count > 0) _tourPurchaseTokenRepository.AddRange(tokens);

        _orderItemRepository.RemoveRange(shoppingCart.OrdersId);
        _shoppingCartRepository.Delete(shoppingCartId);

        return Result.Ok();
    }
}