﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? Duration { get; set; }
        public double? Distance { get; set; }
        public string Difficulty { get; set; }
        public string TransportType { get; set; }
        public string Status { get; set; }
        public List<String>? Tags { get; set; }
        public DateTime StatusUpdateTime { get; set; }
        public List<KeypointDto>? Keypoints { get; set; }
    }
}
