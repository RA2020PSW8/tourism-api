using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubJoinRequest: Entity
    {
        public long UserId { get; private set; }
        //public User User { get; private set; }
        public long ClubId { get; private set; }
        //club object
        public JoinRequestStatus Status { get; private set; }

        public ClubJoinRequest(long userId, long clubId, JoinRequestStatus status)
        {
            UserId = userId;
            //User = user;
            ClubId = clubId;
            //Club = club;
            Status = status;
        }
    }
    public enum JoinRequestStatus
    {
        Accepted,
        Rejected,
        Pending,
        Canceled
    }
}
