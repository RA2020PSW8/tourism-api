using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Notification : Entity
    {
        public int UserId { get; init; }
        public String Content { get; init; }
        public String? ActionURL { get; init; }
        public DateTime CreationDateTime { get; init; }
        public bool IsRead { get; private set; }

        public Notification(int userId, string content, string actionURL, DateTime creationDateTime, bool isRead)
        {
            UserId = userId;
            Content = content;
            ActionURL = actionURL;
            CreationDateTime = creationDateTime;
            IsRead = isRead;
        }
    }
}
