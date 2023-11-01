using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Enum;

namespace Explorer.Tours.Core.Domain;

public class Keypoint : Entity
{
    public string Name { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public string? Description { get; init; }
    public KeypointStatus Status { get; init; }

    public Keypoint()
    {
        
    }
    
    public Keypoint(string name, double latitude, double longitude, string? description, KeypointStatus status)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid name");
        if (latitude is > 90 or < -90) throw new ArgumentException("Invalid latitude");
        if (longitude is > 180 or < -180) throw new ArgumentException("Invalid longitude");
        
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        Status = status;
    }
}