using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class TourIssueComment : Entity
    {

        public string Comment { get; init; }
        public DateTime CreationDateTime { get; init; }
        public long TourIssueId { get; init; }
        public long UserId { get; init; }
        public TourIssueComment()
        {

        }

        public TourIssueComment(string comment, DateTime creationDateTime, int tourIssueId, int userId)
        {
            if(string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException("Comment must not be empty!");
            Comment = comment;
            CreationDateTime = creationDateTime;
            TourIssueId = tourIssueId;
            UserId = userId;
        }
    }
}
