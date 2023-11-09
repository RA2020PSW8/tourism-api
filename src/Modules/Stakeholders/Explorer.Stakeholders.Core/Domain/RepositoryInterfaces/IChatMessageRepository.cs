using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IChatMessageRepository: ICrudRepository<ChatMessage>
    {
        PagedResult<ChatMessage> GetConversation(long firstParticipantId, long secondParticipantId);
    }
}
