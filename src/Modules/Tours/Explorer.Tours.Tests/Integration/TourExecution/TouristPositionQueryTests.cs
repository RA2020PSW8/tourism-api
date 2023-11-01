using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.MarketPlace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.API.Controllers.Tourist.TourExecution;
using Explorer.Tours.API.Public.TourExecution;

namespace Explorer.Tours.Tests.Integration.TourExecution
{
    [Collection("Sequential")]
    public class TouristPositionQueryTests : BaseToursIntegrationTest
    {
        public TouristPositionQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves()
        {
            // Arrange
            int userId = 3;
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);

            // Act
            var result = ((ObjectResult)controller.GetByUser().Result)?.Value as TouristPositionDto;

            // Assert
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(userId);
            result.Latitude.ShouldBe(20);
            result.Longitude.ShouldBe(30);
        }

        [Fact]
        public void Retrieves_fails()
        {
            // Arrange
            int userId = 10;
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);

            // Act
            var result = (ObjectResult)controller.GetByUser().Result;

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
