using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourPreferenceDto
    {
        public long UserId { get; set; }
        public TourDifficulty? Difficulty { get; set; }
        public TransportType? TransportType { get; set; }
        public List<string> Tags { get; set; }
    }

    public enum TourDifficulty
    {
        EASY,
        MEDIUM,
        HARD,
        EXTREME
    }

    public enum TransportType
    {
        WALK,
        BIKE,
        CAR,
        BOAT
    }
}
