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
        public int TourID { get; init; }
        public int UserID { get; init; }
        public required string Comment { get; init; }
        public DateTime CreationDateTime { get; init; }

        public TourIssueComment()
        {

        }

        public TourIssueComment(int tourID, int userID, string comment, DateTime creationDateTime)
        {
            TourID = tourID;
            UserID = userID;
            Comment = comment;
            CreationDateTime = creationDateTime;
        }
    }
}
