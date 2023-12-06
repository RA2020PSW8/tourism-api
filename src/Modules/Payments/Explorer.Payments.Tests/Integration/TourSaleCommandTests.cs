using Explorer.API.Controllers.Tourist.Marketplace;
using Explorer.Payments.Core.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Payments.Tests.Integration;

public class TourSaleCommandTests : BasePaymentsIntegrationTest
{
    public TourSaleCommandTests(PaymentsTestFactory factory) : base(factory)
    {
    }



    private static TourSaleController CreateController(IServiceScope scope)
    {
        return new TourSaleController(scope.ServiceProvider.GetRequiredService<TourSaleService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}