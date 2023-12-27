using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using System.Text;

namespace Explorer.Payments.Core.UseCases;

public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
{
    protected readonly IOrderItemRepository _orderItemRepository;
    protected readonly IShoppingCartRepository _shoppingCartRepository;
    protected readonly ITourPurchaseTokenRepository _tourPurchaseTokenRepository;
    protected readonly IPaymentRecordRepository _paymentRecordRepository;
    protected readonly IWalletRepository _walletRepository;
    protected readonly IInternalNotificationService _notificationService;
    protected readonly IInternalEmailService _emailService;
    protected readonly IInternalUserService _userService;


    public TourPurchaseTokenService(IOrderItemRepository orderItemRepository,
        IShoppingCartRepository shoppingCartRepository, ITourPurchaseTokenRepository repository, IPaymentRecordRepository paymentRecordRepository,
        IWalletRepository walletRepository,
        IInternalNotificationService notificationService,
        IInternalEmailService emailService,
        IInternalUserService userService,
        IMapper mapper) : base(repository, mapper)
    {
        _tourPurchaseTokenRepository = repository;
        _shoppingCartRepository = shoppingCartRepository;
        _orderItemRepository = orderItemRepository;
        _paymentRecordRepository = paymentRecordRepository;
        _walletRepository = walletRepository;
        _notificationService = notificationService;
        _emailService = emailService;   
        _userService = userService;
    }


    public Result BuyShoppingCart(int shoppingCartId)
    {
        ShoppingCart shoppingCart = GetShoppingCart(shoppingCartId);

        if(shoppingCart == null)
            return Result.Fail(FailureCode.NotFound).WithError("Shopping cart does not exist!");


        var tokens = new List<TourPurchaseToken>();
        var paymentRecords = new List<PaymentRecord>();
        var orderItems = new List<OrderItem>(); // for email message


        foreach (var orderId in shoppingCart.OrdersId)
        {
            OrderItem orderItem = GetOrderItem(orderId); 

            if (orderItem == null)
                return Result.Fail(FailureCode.NotFound).WithError("Order item does not exist!");


            if (CheckIfPurchased(orderItem.TourId, shoppingCart.UserId).Value)
                return Result.Fail(FailureCode.NotFound).WithError("Token already exists!");

            orderItems.Add(orderItem); 

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

        UserDto user = _userService.Get(shoppingCart.UserId).Value;
        _emailService.SendEmail(user.Email, "You have successfully completed your purchase!", GetMessageText(orderItems, shoppingCart.Price));

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

    public string GetMessageText(List<OrderItem> orderItems)
    {
        return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Congratulations on Your Adventure Coins!</title>\r\n    <style>\r\n        body {\r\n            font-family: 'Arial', sans-serif;\r\n            background-color: #f4f4f4;\r\n            color: #333;\r\n            margin: 0;\r\n            padding: 0;\r\n        }\r\n\r\n        .container {\r\n            max-width: 600px;\r\n            margin: 20px auto;\r\n            background-color: #fff;\r\n            padding: 20px;\r\n            border-radius: 8px;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n        }\r\n\r\n        h1 {\r\n            color: #3498db; /* Blue color for the title */\r\n            text-align: center;\r\n        }\r\n\r\n        p {\r\n            font-size: 16px;\r\n            line-height: 1.6;\r\n            text-align: justify;\r\n        }\r\n\r\n        .button {\r\n            display: inline-block;\r\n            padding: 10px 20px;\r\n            font-size: 16px;\r\n            text-align: center;\r\n            text-decoration: none;\r\n            background-color: #4CAF50;\r\n            color: #fff;\r\n            border-radius: 5px;\r\n            transition: background-color 0.3s;\r\n        }\r\n\r\n        .button:hover {\r\n            background-color: #45a049;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>New purchase!</h1>\r\n        <p>You have just been gifted Adventure Coins! Get ready for an exciting journey filled with thrilling experiences and epic quests.</p>\r\n        <p>Use your Adventure Coins wisely and make the most of every adventure that comes your way.</p>\r\n        <p>Happy exploring!</p>\r\n    </div>\r\n</body>\r\n</html>\r\n";
    }

    public string GetMessageText(List<OrderItem> orderItems, double totalPurchaseAmount)
    {
        StringBuilder messageBuilder = new StringBuilder();

        messageBuilder.AppendLine("<!DOCTYPE html>");
        messageBuilder.AppendLine("<html lang=\"en\">");
        messageBuilder.AppendLine("<head>");
        messageBuilder.AppendLine("    <meta charset=\"UTF-8\">");
        messageBuilder.AppendLine("    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
        messageBuilder.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        messageBuilder.AppendLine("    <title>Congratulations on Your Adventure Coins Purchase!</title>");
        messageBuilder.AppendLine("    <style>");
        messageBuilder.AppendLine("        body {");
        messageBuilder.AppendLine("            font-family: 'Arial', sans-serif;");
        messageBuilder.AppendLine("            background-color: #f4f4f4;");
        messageBuilder.AppendLine("            color: #333;");
        messageBuilder.AppendLine("            margin: 0;");
        messageBuilder.AppendLine("            padding: 0;");
        messageBuilder.AppendLine("        }");
        messageBuilder.AppendLine("");
        messageBuilder.AppendLine("        .container {");
        messageBuilder.AppendLine("            max-width: 600px;");
        messageBuilder.AppendLine("            margin: 20px auto;");
        messageBuilder.AppendLine("            background-color: #fff;");
        messageBuilder.AppendLine("            padding: 20px;");
        messageBuilder.AppendLine("            border-radius: 8px;");
        messageBuilder.AppendLine("            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);");
        messageBuilder.AppendLine("        }");
        messageBuilder.AppendLine("");
        messageBuilder.AppendLine("        h1 {");
        messageBuilder.AppendLine("            color: #3498db;");
        messageBuilder.AppendLine("            text-align: center;");
        messageBuilder.AppendLine("        }");
        messageBuilder.AppendLine("");
        messageBuilder.AppendLine("        p {");
        messageBuilder.AppendLine("            font-size: 16px;");
        messageBuilder.AppendLine("            line-height: 1.6;");
        messageBuilder.AppendLine("            text-align: justify;");
        messageBuilder.AppendLine("        }");
        messageBuilder.AppendLine("");
        messageBuilder.AppendLine("        .button {");
        messageBuilder.AppendLine("            display: inline-block;");
        messageBuilder.AppendLine("            padding: 10px 20px;");
        messageBuilder.AppendLine("            font-size: 16px;");
        messageBuilder.AppendLine("            text-align: center;");
        messageBuilder.AppendLine("            text-decoration: none;");
        messageBuilder.AppendLine("            background-color: #4CAF50;");
        messageBuilder.AppendLine("            color: #fff;");
        messageBuilder.AppendLine("            border-radius: 5px;");
        messageBuilder.AppendLine("            transition: background-color 0.3s;");
        messageBuilder.AppendLine("        }");
        messageBuilder.AppendLine("");
        messageBuilder.AppendLine("        .button:hover {");
        messageBuilder.AppendLine("            background-color: #45a049;");
        messageBuilder.AppendLine("        }");
        messageBuilder.AppendLine("    </style>");
        messageBuilder.AppendLine("</head>");
        messageBuilder.AppendLine("<body>");
        messageBuilder.AppendLine("    <div class=\"container\">");
        messageBuilder.AppendLine("        <h1>Purchase Confirmation</h1>");
        messageBuilder.AppendLine("        <p>Thank you for your purchase! </p>");
        messageBuilder.AppendLine("        <p>Your purchased tours:</p>");
        messageBuilder.AppendLine("        <ul>");

        foreach (var orderItem in orderItems)
        {
            messageBuilder.AppendLine($"            <li>{orderItem.TourName} - Price: {orderItem.TourPrice:C}</li>");
        }

        messageBuilder.AppendLine("        </ul>");
        messageBuilder.AppendLine($"        <p>Total Purchase Amount: {totalPurchaseAmount:C}</p>");
        messageBuilder.AppendLine("        <p>Get ready for exciting adventures and epic quests.</p>");
        messageBuilder.AppendLine("        <p>Happy exploring!</p>");
        messageBuilder.AppendLine("    </div>");
        messageBuilder.AppendLine("</body>");
        messageBuilder.AppendLine("</html>");
        

        return messageBuilder.ToString();
    }

}