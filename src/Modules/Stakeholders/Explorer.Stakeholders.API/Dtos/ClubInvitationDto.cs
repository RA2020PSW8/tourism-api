
namespace Explorer.Stakeholders.API.Dtos
{
    public enum InvitationStatus { PENDING, ACCEPTED, DENIED, CANCELLED }

    public class ClubInvitationDto
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public int TouristId { get; set; }
        public InvitationStatus Status { get; set; }
    }
}

