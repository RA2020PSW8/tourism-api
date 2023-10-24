using Explorer.Tours.API.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourPreferenceDto
    {
        public long Id { get; set; }
        public long UserId { get; set; } 
        public TourDifficulty Difficulty { get; set; }
        public TransportType TransportType { get; set; }
        public List<string> Tags { get; set; }
    }
}
