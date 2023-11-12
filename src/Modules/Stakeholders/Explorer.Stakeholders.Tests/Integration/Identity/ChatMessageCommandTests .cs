using Explorer.API.Controllers;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Identity;
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
    public class ChatMessageCommandTests : BaseStakeholdersIntegrationTest
    {
        public ChatMessageCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData(-22, -21, "Pozdrav", 200, 2)]
        [InlineData(-21, -25, "Pozdrav", 404, 0)]
        public void Create(int senderId, int receiverId, string content, int expectedResponseCode, int expectedchatMessagesCount)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, senderId);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var message = new MessageDto
            {
                ReceiverId = receiverId,
                Content = content
            };

            var result = ((ObjectResult)controller.Create(message).Result);

            result.StatusCode.ShouldBe(expectedResponseCode);

            var recieverMessages = dbContext.ChatMessages.Where(i => i.ReceiverId == receiverId).ToList();
            recieverMessages.Count.ShouldBe(expectedchatMessagesCount);
        }

        [Theory]
        [InlineData(-22, 0, 404)]
        public void MarkAsRead(int recieverId, int messageId, int expectedResponseCode)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, recieverId);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var result = ((ObjectResult)controller.MarkAsRead(messageId).Result);

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
        }
        private static ChatController CreateController(IServiceScope scope, int userId)
        {
            return new ChatController(scope.ServiceProvider.GetRequiredService<IChatMessageService>())
            {
                ControllerContext = BuildContext(userId.ToString())
            };
        }
    }
}
