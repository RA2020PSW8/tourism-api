﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourPreference : Entity
    {
        public long UserId { get; init; }
        public TourDifficulty? Difficulty { get; init; }
        public TransportType? TransportType { get; init; }
        public List<string> Tags { get; init; }

        public TourPreference()
        {
            Tags = new List<string>();
        }

        public TourPreference(long userId, TourDifficulty difficulty, TransportType transportType, List<string> tags)
        {
            UserId = userId;
            Difficulty = difficulty;
            TransportType = transportType;
            Tags = tags;

            Validate();
        }

        private void Validate()
        {
            if (UserId <= 0) throw new ArgumentException("Invalid UserId");
        }
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