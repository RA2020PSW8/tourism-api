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
            var reciever = _personRepository.Get(message.ReceiverId);
            var sender = _personRepository.Get(senderId);

            var newMessage = new ChatMessage(senderId, message.ReceiverId, message.Content, DateTime.UtcNow, false);
            var result = _chatRepository.Create(newMessage);

            return MapToDto(result);
        }

        public Result<PagedResult<ChatMessageDto>> GetConversation(long firstParticipantId, long secondpParticipantId)
        {
            var results = _chatRepository.GetConversation(firstParticipantId, secondpParticipantId);
            return MapToDto(results);
        }
        public Result<ChatMessageDto> MarkAsRead(long recieverId, long messageId)
        {
            var message = _chatRepository.Get(messageId);
            message.MarkAsRead(recieverId);
            var result = _chatRepository.Update(message);

            return MapToDto(result);
        }
    }
}
