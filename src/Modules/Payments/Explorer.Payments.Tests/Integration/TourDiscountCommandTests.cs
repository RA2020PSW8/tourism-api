using Explorer.API.Controllers.Tourist.Marketplace;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration;

public class TourDiscountCommandTests : BasePaymentsIntegrationTest
{

    public TourDiscountCommandTests(PaymentsTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

        var tourDiscount = new TourDiscountDto
        {
            DiscountId = -3,
            TourId = -4
        };

        var result = ((ObjectResult)controller.Create(tourDiscount).Result)?.Value as TourDiscountDto;

        // Assert - Response
        result.ShouldNotBeNull();

        // Assert - Database
        var storedEntity = dbContext.TourDiscounts.FirstOrDefault(i => i.TourId == tourDiscount.TourId);
        storedEntity.ShouldNotBeNull();
    }
    [Fact]
    public void Deletes()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

        var result = ((OkResult)controller.Delete(-1));
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.Discounts.FirstOrDefault(i => i.Id == -1);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (OkResult)controller.Delete(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }


    private static TourDiscountController CreateController(IServiceScope scope)
    {
        return new TourDiscountController(scope.ServiceProvider.GetRequiredService<ITourDiscountService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}