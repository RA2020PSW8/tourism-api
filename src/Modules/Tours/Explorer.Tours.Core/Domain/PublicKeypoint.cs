using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PublicKeypoint : Entity
    {
        public string Name { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string? Description { get; init; }
        public int? Position { get; init; }
        public string? Image { get; set; }

        public PublicKeypoint()
        {

        }

        public PublicKeypoint(string name, double latitude, double longitude, string? description, int? position, string? image)
        {
            Validate(name, latitude, longitude, position);

            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Description = description;
            Position = position;
            Image = image;
        }

        private static void Validate(string name, double latitude, double longitude, int? position)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid name");
            if (latitude is > 90 or < -90) throw new ArgumentException("Invalid latitude");
            if (longitude is > 180 or < -180) throw new ArgumentException("Invalid longitude");
            if (position < 1) throw new ArgumentException("Invalid position");
        }
    }
}
