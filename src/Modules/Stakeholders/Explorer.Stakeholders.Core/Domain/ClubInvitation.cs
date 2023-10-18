using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum InvitationStatus { PENDING, ACCEPTED, DENIED, CANCELLED }

    public class ClubInvitation : Entity
    {
        public int ClubId { get; init; }
        public int TouristId { get; init; }
        public InvitationStatus Status { get; init; }

        public ClubInvitation()
        {
        }

        public ClubInvitation(int clubId, int touristId, InvitationStatus status)
        {

            ClubId = clubId;
            TouristId = touristId;
            Status = status;
        }
    }
}