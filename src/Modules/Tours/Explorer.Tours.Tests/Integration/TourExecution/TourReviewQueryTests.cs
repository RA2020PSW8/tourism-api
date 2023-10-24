using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourExecution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.TourExecution
{
    [Collection("Sequential")]
    public class TourReviewQueryTests : BaseToursIntegrationTest
    {
        public TourReviewQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            //Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourReviewDto>;

            //Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        private static Explorer.API.Controllers.Tourist.TourExecution.TourReviewController CreateController(IServiceScope scope)
        {
            return new Explorer.API.Controllers.Tourist.TourExecution.TourReviewController(scope.ServiceProvider.GetRequiredService<ITourReviewService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
