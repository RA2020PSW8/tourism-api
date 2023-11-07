using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Notifications
{
    [Collection("Sequential")]
    public class NotificationCommandTests : BaseStakeholdersIntegrationTest
    {
        public NotificationCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        
        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new NotificationDto
            {
                Id = -1,
                UserId = 1,
                Content = "New notification has been updated",
                ActionURL = "url",
                CreationDateTime = DateTime.UtcNow,
                IsRead = false
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as NotificationDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.IsRead.ShouldBe(updatedEntity.IsRead);
            result.Content.ShouldBe(updatedEntity.Content);

            // Assert - Database
            var storedEntity = dbContext.Notifications.FirstOrDefault(n => n.CreationDateTime == updatedEntity.CreationDateTime);
            storedEntity.ShouldNotBeNull();
            storedEntity.IsRead.ShouldBe(updatedEntity.IsRead);
            storedEntity.Content.ShouldBe(updatedEntity.Content);          
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new NotificationDto
            {
                Id = -1000,
                UserId = 1,
                Content = "New notification has been updated",
                ActionURL = "url",
                CreationDateTime = DateTime.UtcNow,
                IsRead = false
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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Notifications.FirstOrDefault(i => i.Id == -3);
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

        private static NotificationController CreateController(IServiceScope scope)
        {
            return new NotificationController(scope.ServiceProvider.GetRequiredService<INotificationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
        
    }
}
