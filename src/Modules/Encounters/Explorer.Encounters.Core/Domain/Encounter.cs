using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.Core.Domain.Enum;

namespace Explorer.Encounters.Core.Domain
{
    public class Encounter : Entity
    {
        public int UserId { get; init; }
        public String Name { get; init; }
        public String Description {  get; init; }
        public double Latitude {  get; init; }
        public double Longitude { get; init; }
        public int Xp {  get; init; }
        public EncounterStatus Status { get; init; }
        public EncounterType Type { get; init; }

        public Encounter() { }

        public Encounter(int userId, string name, string description, double latitude, double longitude, int xp, EncounterStatus status, EncounterType type)
        {
            UserId = userId;
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            Xp = xp;
            Status = status;
            Type = type;
        }
    }
}
