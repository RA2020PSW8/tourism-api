using Explorer.API.Controllers.Tourist.Marketplace;
using Explorer.Payments.Core.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Payments.Tests.Integration;

public class SaleQueryTests:BasePaymentsIntegrationTest
{
    public SaleQueryTests(PaymentsTestFactory factory) : base(factory)
    {
    }

    private static SaleController CreateController(IServiceScope scope)
    {
        return new SaleController(scope.ServiceProvider.GetRequiredService<SaleService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}