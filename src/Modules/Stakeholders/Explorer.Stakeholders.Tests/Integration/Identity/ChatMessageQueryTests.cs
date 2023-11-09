using Explorer.API.Controllers;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Identity;
using Explorer.Stakeholders.API.Public.Tourist;
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
    [Collection("Sequential")]
    public class ChatMessageQueryTests : BaseStakeholdersIntegrationTest
    {
            public ChatMessageQueryTests(StakeholdersTestFactory factory) : base(factory) { }

            [Fact]
            public void Retrieves_all()
            {
                // Arrange
                using var scope = Factory.Services.CreateScope();
                var controller = CreateController(scope);

                // Act
                var result = ((ObjectResult)controller.GetConversation(-22).Result)?.Value as PagedResult<ChatMessageDto>;

                // Assert
                result.ShouldNotBeNull();
                result.Results.Count.ShouldBe(3);
                result.TotalCount.ShouldBe(3);
            }

            private static ChatController CreateController(IServiceScope scope)
            {
                return new ChatController(scope.ServiceProvider.GetRequiredService<IChatMessageService>())
                {
                    ControllerContext = BuildContext("-21")
                };
            }
        }
}
