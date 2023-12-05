using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class EncounterCompletionDto
    {
        public long UserId { get; set; }
        public DateTime CompletionTime { get; set; }
        public EncounterDto Encounter { get; set; }
        public int Xp { get; init; }
        public string Status { get; set; }
    }
}
