using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDto
    {
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public double Price { get; init; }
        public string Difficulty { get; init; }
        public string TransportType { get; init; }
        public string Status { get; init; }
        public List<String> Tags { get; init; }
    
        public TourDto()
        {
            Tags = new List<String>();
        }

        public TourDto(long userId, string name, string description, double price, string difficulty, string transportType, string status, List<string> tags)
        {
            UserId = userId;
            Name = name;
            Description = description;
            Price = price;
            Difficulty = difficulty;
            TransportType = transportType;
            Status = status;
            Tags = tags;

            Validate();
        }

        public void Validate()
        {
            if (Price < 0) throw new ArgumentException("Invalid price.");
            if (UserId < 0) throw new ArgumentException("Invalid userId.");
            if (string.IsNullOrEmpty(Description)) throw new ArgumentException("Invalid description.");
            if (!(Difficulty == "EASY" || Difficulty == "MEDIUM" || Difficulty == "HARD" || Difficulty == "EXTREME")) throw new ArgumentException("Invalid difficulty");
            if (!(TransportType == "WALK" || TransportType == "BIKE" || TransportType == "CAR" || TransportType == "BOAT")) throw new ArgumentException("Invalid transport type");
            if (!(Status == "DRAFT")) throw new ArgumentException("Invalid status.");
            if (string.IsNullOrEmpty(Name)) throw new ArgumentException("Invalid name");
        }


    }
}
