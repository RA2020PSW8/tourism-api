using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Public.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases.Identity
{
    public class ChatMessageService: CrudService<ChatMessageDto, ChatMessage>, IChatMessageService
    {
        protected readonly IChatMessageRepository _chatRepository;
        protected readonly IPersonRepository _personRepository;
        public ChatMessageService(IChatMessageRepository chatRepository, IPersonRepository personRepository, IMapper mapper) : base(chatRepository, mapper)
        {
            _chatRepository = chatRepository;
            _personRepository = personRepository;
        }

        public Result<ChatMessageDto> Create(long senderId, MessageDto message)
        {
            if(!_personRepository.Exists(message.ReceiverId)) return Result.Fail(FailureCode.NotFound).WithError("Reciever doesn't exist");
            if (!_personRepository.Exists(senderId)) return Result.Fail(FailureCode.NotFound).WithError("Sender doesn't exist");

            var newMessage = new ChatMessage(senderId, message.ReceiverId, message.Content, DateTime.UtcNow, false);
            var result = _chatRepository.Create(newMessage);

            return MapToDto(result);
        }

        public Result<List<ChatMessageDto>> GetPreviewMessages(long userId)
        {
            var results = _chatRepository.GetPreviewMessages(userId);
            return MapToDto(results.ToList());
        }

        public Result<PagedResult<ChatMessageDto>> GetConversation(long firstParticipantId, long secondParticipantId)
        {
            foreach(ChatMessage message in _chatRepository.GetChatUnreadMessages(secondParticipantId, firstParticipantId))
            {
                message.MarkAsRead();
                _chatRepository.Update(message);
            }
            var results = _chatRepository.GetConversation(firstParticipantId, secondParticipantId);
            
            return MapToDto(results);
        }
    }
}
