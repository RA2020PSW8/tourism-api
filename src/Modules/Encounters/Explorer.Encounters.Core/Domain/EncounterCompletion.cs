﻿using Explorer.BuildingBlocks.Core.Domain;
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
        public EncounterCompletionStatus Status { get; private set; }

        public EncounterCompletion() { }

        public EncounterCompletion(long userId, long encounterId, int xp, EncounterCompletionStatus status)
        {
            UserId = userId;
            EncounterId = encounterId;
            CompletionTime = DateTime.UtcNow;
            Xp = xp;
            Status = status;
        }

        public void UpdateStatus(EncounterCompletionStatus status)
        {
            Status = status;
        }
    }
}
