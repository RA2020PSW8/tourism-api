using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
{
    protected readonly IOrderItemRepository _orderItemRepository;
    protected readonly IShoppingCartRepository _shoppingCartRepository;
    protected readonly ITourPurchaseTokenRepository _tourPurchaseTokenRepository;
    protected readonly IPaymentRecordRepository _paymentRecordRepository;
    protected readonly IWalletRepository _walletRepository;
    protected readonly IInternalNotificationService _notificationService;
    protected readonly ICouponRepository _couponRepository;

    public TourPurchaseTokenService(IOrderItemRepository orderItemRepository,
        IShoppingCartRepository shoppingCartRepository, ITourPurchaseTokenRepository repository, IPaymentRecordRepository paymentRecordRepository,
        IWalletRepository walletRepository,
        IInternalNotificationService notificationService,
        ICouponRepository couponRepository,
        IMapper mapper) : base(repository, mapper)
    {
        _tourPurchaseTokenRepository = repository;
        _shoppingCartRepository = shoppingCartRepository;
        _orderItemRepository = orderItemRepository;
        _paymentRecordRepository = paymentRecordRepository;
        _walletRepository = walletRepository;
        _notificationService = notificationService;
        _couponRepository = couponRepository;
    }


    public Result BuyShoppingCart(int shoppingCartId, List<CouponDto> selectedCoupons)
    {
        ShoppingCart shoppingCart = GetShoppingCart(shoppingCartId);
        shoppingCart.Price = 0;

        if (shoppingCart == null)
            return Result.Fail(FailureCode.NotFound).WithError("Shopping cart does not exist!");

        var tokens = new List<TourPurchaseToken>();
        var paymentRecords = new List<PaymentRecord>();

        foreach (var orderId in shoppingCart.OrdersId)
        {
            OrderItem orderItem = GetOrderItem(orderId);

            if (orderItem == null)
                return Result.Fail(FailureCode.NotFound).WithError("Order item does not exist!");

            shoppingCart.Price += orderItem.TourPrice;

            if (CheckIfPurchased(orderItem.TourId, shoppingCart.UserId).Value)
                return Result.Fail(FailureCode.NotFound).WithError("Token already exists!");

            if(selectedCoupons.Count > 0)
            {
                foreach (var coupon in selectedCoupons)
                {

                    if (coupon != null && coupon.TourId == orderItem.TourId)
                    {
                        var discountedPrice = orderItem.TourPrice * (coupon.Discount / 100);
                        orderItem.TourPrice -= discountedPrice;
                        shoppingCart.Price -= discountedPrice;
                    }
                }
            }

            var token = new TourPurchaseToken(orderItem.TourId, shoppingCart.UserId);
            tokens.Add(token);

            var paymentRecord = new PaymentRecord(orderItem.TourId, shoppingCart.UserId, orderItem.TourPrice, DateTimeOffset.Now.ToUniversalTime());
            paymentRecords.Add(paymentRecord);

        }

        Wallet wallet = _walletRepository.GetByUser(shoppingCart.UserId);
        wallet.AdventureCoins = wallet.AdventureCoins - shoppingCart.Price;
        _walletRepository.Update(wallet);

        AddTokensToRepository(tokens);
        AddPaymentRecordsToRepository(paymentRecords);

        _notificationService.Generate(shoppingCart.UserId, Stakeholders.API.Dtos.Enums.NotificationType.TOUR_PURCHASED, "", DateTime.UtcNow, "");

        RemoveOrderItems(shoppingCart.OrdersId);
        DeleteShoppingCart(shoppingCartId);

        if (selectedCoupons.Count > 0)
        {
            foreach (var coupon in selectedCoupons)
            {
                _couponRepository.Delete(coupon.Id);
            }
        }

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
    
    private void AddPaymentRecordsToRepository(List<PaymentRecord> paymentRecords)
    {
        if (paymentRecords.Count > 0)
        {
            _paymentRecordRepository.AddRange(paymentRecords);
        }
    }

    public Result<bool> CheckIfPurchased(long tourId, long userId)
    {
        return _tourPurchaseTokenRepository.CheckIfPurchased(tourId, userId);
    }
}