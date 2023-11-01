using Explorer.API.Controllers.Tourist.TourExecution;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.API.Public.TourExecution;

namespace Explorer.Tours.Tests.Integration.TourExecution
{
    [Collection("Sequential")]
    public class TouristPositionCommandTests : BaseToursIntegrationTest
    {
        public TouristPositionCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            int userId = 5;
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TouristPositionDto
            {
                Longitude = 20,
                Latitude = 20
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TouristPositionDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);

            // Assert - Database
            var storedEntity = dbContext.TouristPositions.FirstOrDefault(tp => tp.UserId == userId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_position_exists()
        {
            // Arrange
            int userId = 3;
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);
            var newEntity = new TouristPositionDto
            {
                Longitude = 20,
                Latitude = 20
            };

            // Act
            var result = (ObjectResult)controller.Create(newEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(409);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            int userId = 3;
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TouristPositionDto
            {
                Longitude = 20,
                Latitude = 20
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TouristPositionDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.Longitude.ShouldBe(updatedEntity.Longitude);
            result.Latitude.ShouldBe(updatedEntity.Latitude);

            // Assert - Database
            var storedEntity = dbContext.TouristPositions.FirstOrDefault(tp => tp.UserId == userId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
            storedEntity.Longitude.ShouldBe(updatedEntity.Longitude);
            storedEntity.Latitude.ShouldBe(updatedEntity.Latitude);
        }

        [Fact]
        public void Update_fails_preference_missing()
        {
            // Arrange
            int userId = 4;
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TouristPositionDto
            {
                Longitude = 20,
                Latitude = 20
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static TouristPositionController CreateController(IServiceScope scope, int userId)
        {
            return new TouristPositionController(scope.ServiceProvider.GetRequiredService<ITouristPositionService>())
            {
                ControllerContext = BuildContext(userId.ToString())
            };
        }
    }
}
