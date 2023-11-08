using Explorer.API.Controllers;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.Core.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Identity
{
    public class ProfileCommandTests : BaseStakeholdersIntegrationTest
    {
        public ProfileCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData(-22, -21, 200, 1)]
        [InlineData(-21, -22, 400, 1)]
        public void Follow(int followerId, int followedId, int expectedResponseCode, int expectedFollowingsCount)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, followerId);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            
            var result = ((ObjectResult)controller.Follow(followedId).Result);

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            var updatedFollower = dbContext.People.FirstOrDefault(i => i.Id == followerId);
            updatedFollower.Following.Count.ShouldBe(expectedFollowingsCount);
        }

        [Theory]
        [InlineData(-21, -22, 200, 0)]
        [InlineData(-23, -22, 400, 1)]
        public void Unfollow(int followerId, int followedId, int expectedResponseCode, int expectedFollowingsCount)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, followerId);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var result = ((ObjectResult)controller.Unfollow(followedId).Result);

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            var updatedFollower = dbContext.People.FirstOrDefault(i => i.Id == followerId);
            updatedFollower.Following.Count.ShouldBe(expectedFollowingsCount);
        }
        private static ProfileController CreateController(IServiceScope scope, int userId)
        {
            return new ProfileController(scope.ServiceProvider.GetRequiredService<IProfileService>())
            {
                ControllerContext = BuildContext(userId.ToString())
            };
        }
    }
}
