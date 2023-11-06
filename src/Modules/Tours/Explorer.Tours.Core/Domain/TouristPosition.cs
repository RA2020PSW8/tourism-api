﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TouristPosition : Entity
    {
        public long UserId { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public DateTime UpdatedAt { get; init; }

        public TouristPosition()
        {

        }

        public TouristPosition(long userId, double latitude, double longitude)
        {
            Validate(latitude, longitude);

            UserId = userId;
            Latitude = latitude;
            Longitude = longitude;
            UpdatedAt = DateTime.Now;
        }

        private static void Validate(double latitude, double longitude)
        {
            if (latitude is > 90 or < -90) throw new ArgumentException("Invalid latitude");
            if (longitude is > 180 or < -180) throw new ArgumentException("Invalid longitude");
        }
    }
}
