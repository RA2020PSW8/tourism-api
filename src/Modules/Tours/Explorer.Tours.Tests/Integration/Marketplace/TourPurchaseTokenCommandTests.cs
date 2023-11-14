using Explorer.API.Controllers.Tourist;
using Explorer.API.Controllers.Tourist.Marketplace;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Marketplace
{
    public class TourPurchaseTokenCommandTests: BaseToursIntegrationTest
    {
        public TourPurchaseTokenCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((OkResult)controller.BuyShoppingCart(-1));

            // Assert - Response
            result.ShouldNotBeNull();

            int tokensNumber = dbContext.TourPurchaseTokens.Count();
            tokensNumber.ShouldBe(4);

            // Assert - Database
            var storedEntity = dbContext.TourPurchaseTokens.FirstOrDefault(i => i.TourId == -1);
            storedEntity.ShouldNotBeNull();

        }

        [Fact]
        public void Create_fails_invalid_data_shopping_cart()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.BuyShoppingCart(-111));

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Create_fails_invalid_data_order_item()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.BuyShoppingCart(-1));

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Create_fails_invalid_data_token_exists()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.BuyShoppingCart(-1));

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static TourPurchaseTokenController CreateController(IServiceScope scope)
        {
            return new TourPurchaseTokenController(scope.ServiceProvider.GetRequiredService<ITourPurchaseTokenService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
