using Explorer.API.Controllers.Tourist.Marketplace;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Payments.Tests.Integration;

public class TourDiscountQueryTests : BasePaymentsIntegrationTest
{
    public TourDiscountQueryTests(PaymentsTestFactory factory) : base(factory)
    {

    }
    private static TourDiscountController CreateController(IServiceScope scope)
    {
        return new TourDiscountController(scope.ServiceProvider.GetRequiredService<ITourDiscountService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}