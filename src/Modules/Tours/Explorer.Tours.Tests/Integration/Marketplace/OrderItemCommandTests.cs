using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Infrastructure.Database;
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
    [Collection("Sequential")]
    public class OrderItemCommandTests : BaseToursIntegrationTest
    {
        public OrderItemCommandTests(ToursTestFactory factory) : base(factory) { }
        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new OrderItemDto
            {
                TourId = -1,
                TourName = "Mid",
                TourPrice = 10,
                TourDescription = "Opis ture",
                UserId = 2
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as OrderItemDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(-1);

            // Assert - Database
            var storedEntity = dbContext.OrderItems.FirstOrDefault(i => i.UserId == newEntity.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
        
        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new OrderItemDto
            {
                Id = -1,
                TourId = -1,
                TourDescription = "Kul",
                UserId = -2,
                TourPrice = 150,
                TourName = "Tura"
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as OrderItemDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.TourName.ShouldBe(updatedEntity.TourName);
            result.TourDescription.ShouldBe(updatedEntity.TourDescription);
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.TourId.ShouldBe(updatedEntity.TourId);
            result.TourPrice.ShouldBe(updatedEntity.TourPrice);

            // Assert - Database
            var storedEntity = dbContext.OrderItems.FirstOrDefault(i => i.UserId == -2);
            storedEntity.ShouldNotBeNull();
            storedEntity.TourDescription.ShouldBe(updatedEntity.TourDescription);
            var oldEntity = dbContext.OrderItems.FirstOrDefault(i => i.UserId == -11);
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new OrderItemDto
            {
                Id = -1000,
                TourName = "L klub"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        private static OrderItemController CreateController(IServiceScope scope)
        {
            return new OrderItemController(scope.ServiceProvider.GetRequiredService<IOrderItemService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
