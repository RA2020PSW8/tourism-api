using Microsoft.AspNetCore.Authorization;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Public.Identity;
using Explorer.Stakeholders.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.UseCases.Identity;
using Explorer.Stakeholders.Infrastructure.Authentication;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "personPolicy")]
    [Route("api/chat")]
    public class ChatController: BaseApiController
    {
        private readonly IChatMessageService _chatService;
        public ChatController(IChatMessageService chatService)
        {
            _chatService = chatService;
        }
        
        [HttpGet("{participantId:long}")]
        public ActionResult<PagedResult<ChatMessageDto>> GetConversation(long participantId)
        {
            var result = _chatService.GetConversation(ClaimsPrincipalExtensions.PersonId(User), participantId);
            return CreateResponse(result);
        }
        
        [HttpPost]
        public ActionResult<ChatMessageDto> Create([FromBody] MessageDto message)
        {
            var result = _chatService.Create(ClaimsPrincipalExtensions.PersonId(User), message);
            return CreateResponse(result);
        }

        [HttpGet("preview")]
        public ActionResult<List<ChatMessageDto>> GetPreviewMessages()
        {
            var result = _chatService.GetPreviewMessages(ClaimsPrincipalExtensions.PersonId(User));
            return CreateResponse(result);
        }
    }
}
