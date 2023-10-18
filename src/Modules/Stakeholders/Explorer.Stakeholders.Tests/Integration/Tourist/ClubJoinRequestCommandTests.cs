using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    [Collection("Sequential")]
    public class ClubJoinRequestCommandTests: BaseStakeholdersIntegrationTest
    {
        public ClubJoinRequestCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ClubJoinRequestDto
            {
                UserId = -21,
                ClubId = -2,
                Status = ClubJoinRequestDto.JoinRequestStatus.Pending
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubJoinRequestDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Status.ShouldBe(newEntity.Status);

            // Assert - Database
            var storedEntity = dbContext.ClubJoinRequests.FirstOrDefault(i => i.UserId == newEntity.UserId && i.ClubId == newEntity.ClubId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(newEntity.Status.ToString());
        }
        /* Zbog medjuzavisnosti prolazi svakako
        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            //ovdje stavi podrazumjevane vrijednosti 0, 0 treba da ubacim u crud provjeru
            var updatedEntity = new ClubJoinRequestDto
            {
                
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }
        */
        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new ClubJoinRequestDto
            {
                Id = -1,
                UserId = -21,
                ClubId = -1,
                Status = ClubJoinRequestDto.JoinRequestStatus.Accepted
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ClubJoinRequestDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.Status.ShouldBe(updatedEntity.Status);

            // Assert - Database
            var storedEntity = (ClubJoinRequest) dbContext.ClubJoinRequests.FirstOrDefault(i => i.UserId == updatedEntity.UserId && i.ClubId == updatedEntity.ClubId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(updatedEntity.Status.ToString());
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubJoinRequestDto
            {
                Id = -100,
                UserId = -22,
                ClubId = -1
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        private static ClubJoinRequestController CreateController(IServiceScope scope)
        {
            return new ClubJoinRequestController(scope.ServiceProvider.GetRequiredService<IClubJoinRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
