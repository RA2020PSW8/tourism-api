using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ChatMessageDatabaseRepository: CrudDatabaseRepository<ChatMessage, StakeholdersContext>, IChatMessageRepository
    {

        private readonly DbSet<ChatMessage> _dbSet;

        public ChatMessageDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<ChatMessage>();
        }
        public PagedResult<ChatMessage> GetConversation(long firstParticipantId, long secondParticipantId)
        {
            var chatMessages = _dbSet
                .AsNoTracking()
                .Where(cm => (cm.SenderId == firstParticipantId && cm.ReceiverId == secondParticipantId) ||
                             (cm.SenderId == secondParticipantId && cm.ReceiverId == firstParticipantId))
                .OrderBy(cm => cm.CreationDateTime)
                .Include("Sender")
                .Include("Receiver")
                .ToList();

            return new PagedResult<ChatMessage>(chatMessages, chatMessages.Count);
        }

        public IEnumerable<ChatMessage> GetMessagesForPreview(long userId)
        {
            var chatMessages = _dbSet
                .AsNoTracking()
                .DistinctBy(c => new { c.SenderId, c.ReceiverId })
                .Where(c => c.SenderId == userId || c.ReceiverId == userId)
                .OrderBy(c => new { c.SenderId, c.ReceiverId, c.CreationDateTime });

            return RemovePreviewDuplicates(chatMessages);
        }

        private IEnumerable<ChatMessage> RemovePreviewDuplicates(IEnumerable<ChatMessage> messages)
        {
            List<ChatMessage> reducedMessages = new List<ChatMessage>();
            foreach (var chatm in messages)
            {
                if (reducedMessages.FirstOrDefault(m => m.SenderId == chatm.ReceiverId || m.ReceiverId == chatm.SenderId) != null)
                    continue;
                
                reducedMessages.Add(
                    messages
                        .Where(m => m.SenderId == chatm.ReceiverId || m.ReceiverId == chatm.SenderId)
                        .OrderBy(m => m.CreationDateTime)
                        .FirstOrDefault()
                    );
            }

            return reducedMessages;
        }
    }
}
