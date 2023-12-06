using Explorer.API.Controllers.Tourist.Marketplace;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration;

public class SaleCommandTests: BasePaymentsIntegrationTest
{

    public SaleCommandTests(PaymentsTestFactory factory) : base(factory)
    {
    }

    public void Creates()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

        var sale = new SaleDto()
        {
            Id = -5,
            StartDate = DateOnly.Parse("1/1/2023"),
            EndDate = DateOnly.Parse("1/1/2023"),
            Percentage = 10,
            UserId = -11
        };

        var result = ((ObjectResult)controller.Create(sale).Result)?.Value as SaleDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Percentage.ShouldBe(sale.Percentage);

        // Assert - Database
        var storedEntity = dbContext.Sales.FirstOrDefault(i => i.Percentage == sale.Percentage);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }
    private static SaleController CreateController(IServiceScope scope)
    {
        return new SaleController(scope.ServiceProvider.GetRequiredService<SaleService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}