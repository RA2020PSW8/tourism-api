using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tour;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Tour;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;


namespace Explorer.Tours.Tests.Integration.Tour
{
    public class TourCommandTests : BaseToursIntegrationTest
    {
        public TourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourDto()
            {
                UserId = 1,
                Name = "Test",
                Description = "Test",
                Price = 0,
                Difficulty = "EASY",
                TransportType = "CAR",
                Status = "DRAFT",
                Tags = new()
            };
        
            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourDto()
            {
                Description = "Update"
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourDto()
            {
                Id = 1,
                UserId = 1,
                Name = "Test",
                Description = "Test",
                Price = 0,
                Difficulty = "EASY",
                TransportType = "CAR",
                Status = "DRAFT",
                Tags = new()
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
            result.Name.ShouldBe(updatedEntity.Name);
            result.Description.ShouldBe(updatedEntity.Description);
            result.Price.ShouldBe(updatedEntity.Price);
            result.Difficulty.ShouldBe(updatedEntity.Difficulty);   
            result.TransportType.ShouldBe(updatedEntity.TransportType);
            result.Status.ShouldBe(updatedEntity.Status);   

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == "Test");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Tours.FirstOrDefault(i => i.Name == "Test123");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourDto()
            {
                Id = -1,
                UserId = 1,
                Name = "Test",
                Description = "Test",
                Price = 0,
                Difficulty = "EASY",
                TransportType = "CAR",
                Status = "DRAFT",
                Tags = new()
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Tours.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
