﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Explorer.Stakeholders.Core.Domain;

public class NewsletterPreference : Entity
{
    public long UserID { get; init; }
    public User User { get; init; }
    public uint Frequency { get; init; }
    public DateTime LastSent { get; init; }

    public NewsletterPreference(long userID, uint frequency, DateTime lastSent)
    {
        UserID = userID;
        Frequency = frequency;
        LastSent = lastSent;
        if (frequency != 0 && frequency != 1 && frequency != 3 && frequency != 7)
            throw new ArgumentNullException("Invalid frequency");
    }
}
