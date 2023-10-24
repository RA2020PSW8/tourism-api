using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    [Collection("Sequential")]

    public class ApplicationRatingCommandTests : BaseStakeholdersIntegrationTest
    {
        public ApplicationRatingCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ApplicationRatingDto
            {
                Id = 12,
                Rating = 4,
                Comment = "Very good!",
                UserId = -1,
                LastModified = DateTime.UtcNow
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ApplicationRatingDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Id.ShouldBe(newEntity.Id);

            // Assert - Database
            var storedEntity = dbContext.ApplicationRatings.FirstOrDefault(i => i.Id == newEntity.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        private static ApplicationRatingController CreateController(IServiceScope scope)
        {
            return new ApplicationRatingController(scope.ServiceProvider.GetRequiredService<IApplicationRatingService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
