using Explorer.Tours.API.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicKeypointDto
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Description { get; set; }
        public int? Position { get; set; }
        public string? Image { get; set; }
        public KeypointStatus Status { get; set; }
    }
}
