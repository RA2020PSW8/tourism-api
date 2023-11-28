using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.Core.Domain.Enums;

namespace Explorer.Encounters.Core.Domain
{
    public class EncounterCompletion : Entity
    {
        public long UserId { get; init; }
        public DateTime CompletionTime { get; init; }
        public long EncounterId { get; }
        public Encounter Encounter { get; }
        public int Xp { get; init; }
        public EncounterCompletionStatus Status { get; init; }

        public EncounterCompletion() { }

        public EncounterCompletion(long userId, long encounterId, int xp, EncounterCompletionStatus status)
        {
            UserId = userId;
            EncounterId = encounterId;
            CompletionTime = DateTime.UtcNow;
            Xp = xp;
            Status = status;
        }
    }
}
