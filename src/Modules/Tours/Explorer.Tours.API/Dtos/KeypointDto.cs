using Explorer.Tours.API.Dtos.Enums;

namespace Explorer.Tours.API.Dtos;

public class KeypointDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; }
    public KeypointStatus Status { get; set; }
}