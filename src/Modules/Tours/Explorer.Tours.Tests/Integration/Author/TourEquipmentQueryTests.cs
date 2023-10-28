using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Tour;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Author
{
    public class TourEquipmentQueryTests : BaseToursIntegrationTest
    {
        public TourEquipmentQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetEquipmentForTour(-1).Result)?.Value as List<EquipmentDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
        }

        private static TourEquipmentController CreateController(IServiceScope scope)
        {
            return new TourEquipmentController(scope.ServiceProvider.GetRequiredService<ITourEquipmentService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
