using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.MarketPlace;
using Explorer.Tours.Core.Domain.Enums;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Tours.Tests.Integration.Marketplace
{
    [Collection("Sequential")]
    public class TourPreferenceQueryTests : BaseToursIntegrationTest
    {
        public TourPreferenceQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves()
        {
            // Arrange
            int userId = 3;
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            controller.ControllerContext = BuildContext(userId.ToString());

            // Act
            var result = ((ObjectResult)controller.GetByUser().Result)?.Value as TourPreferenceDto;

            // Assert
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(userId);
            result.Difficulty.ShouldBe(API.Dtos.Enums.TourDifficulty.MEDIUM);
            result.TransportType.ShouldBe(API.Dtos.Enums.TransportType.BIKE);
            // result.Tags.ShouldBe(); // ????
        }

        [Fact]
        public void Retrieves_fails()
        {
            // Arrange
            int userId = 10;
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            controller.ControllerContext = BuildContext(userId.ToString());

            // Act
            var result = (ObjectResult)controller.GetByUser().Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static TourPreferenceController CreateController(IServiceScope scope)
        {
            return new TourPreferenceController(scope.ServiceProvider.GetRequiredService<ITourPreferenceService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

        new private static ControllerContext BuildContext(string id)
        {
            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                    new Claim("personId", id)
                }))
                }
            };
        }
    }
}
