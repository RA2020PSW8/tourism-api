using Explorer.Blog.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourExecution;
using Explorer.Tours.Infrastructure.Database;
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
    public class TourReviewCommandTests : BaseToursIntegrationTest
    {
        public TourReviewCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourReviewDto
            {
                Rating = 4,
                Comment = "It was amazing!",
                VisitDate = DateTime.Now.ToUniversalTime(),
                RatingDate = DateTime.Now.ToUniversalTime(),
                ImageLinks = new List<string>() { "test" },
            };

            //Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourReviewDto;

            //Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Id.ShouldBe(result.Id);

            //Assert - Database
            var storedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Comment == newEntity.Comment);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourReviewDto
            {
                Rating = -5
            };

            //Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            //Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourReviewDto
            {
                Id = -2,
                Rating = 5,
                Comment = "awesome",
                VisitDate = DateTime.Now.ToUniversalTime(),
                RatingDate = DateTime.Now.ToUniversalTime(),
                ImageLinks = new List<string> { "image2.jpg" }
            };

            //Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourReviewDto;

            //Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.Rating.ShouldBe(updatedEntity.Rating);
            result.Comment.ShouldBe(updatedEntity.Comment);
            result.VisitDate.ShouldBe(updatedEntity.VisitDate);
            result.RatingDate.ShouldBe(updatedEntity.RatingDate);
            result.ImageLinks.ShouldBe(updatedEntity.ImageLinks);

            //Assert - Database
            var storedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Comment == "awesome");
            storedEntity.ShouldNotBeNull();
            storedEntity.Rating.ShouldBe(updatedEntity.Rating);
            var oldEntity = dbContext.TourReviews.FirstOrDefault(i => i.Comment == "ok");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourReviewDto
            {
                Id = -98,
                Rating = 3,
                Comment = "idk"
            };

            //Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            //Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            //Act
            var result = (OkResult)controller.Delete(-1);

            //Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            //Assert - Database
            var deletedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Id == -1);
            deletedEntity.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            //Act
            var result = (ObjectResult)controller.Delete(-97);

            //Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
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
