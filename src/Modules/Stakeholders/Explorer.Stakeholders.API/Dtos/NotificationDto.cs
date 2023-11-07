
namespace Explorer.Stakeholders.API.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public String Content {  get; set; }
        public String? ActionURL { get; set; }
        public bool IsRead { get; set; }
    }
}
