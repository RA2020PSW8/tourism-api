namespace Explorer.Stakeholders.API.Dtos;

public class ClubDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int UserId { get; set; }
    public List<int> MemberIds { get; set; }
}