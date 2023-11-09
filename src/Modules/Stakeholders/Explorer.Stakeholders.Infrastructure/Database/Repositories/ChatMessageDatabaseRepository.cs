using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ChatMessageDatabaseRepository: CrudDatabaseRepository<ChatMessage, StakeholdersContext>, IChatMessageRepository
    {

        private readonly StakeholdersContext _dbContext;

        public ChatMessageDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public PagedResult<ChatMessage> GetConversation(long firstParticipantId, long secondParticipantId)
        {
            var chatMessages = _dbContext.ChatMessages
                .AsNoTracking()
                .Where(cm => (cm.SenderId == firstParticipantId && cm.ReceiverId == secondParticipantId) ||
                             (cm.SenderId == secondParticipantId && cm.ReceiverId == firstParticipantId))
                .OrderBy(cm => cm.CreationDateTime)
                .Include("Sender")
                .Include("Receiver")
                .ToList();

            return new PagedResult<ChatMessage>(chatMessages, chatMessages.Count);
        }
    }
}
