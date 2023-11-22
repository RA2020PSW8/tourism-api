﻿using Explorer.Encounters.API.Dtos.Enums;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Xp { get; set; }
        public EncounterStatus Status { get; set; }
        public EncounterType Type { get; set; }
    }
}
