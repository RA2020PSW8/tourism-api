﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class OrderItem : Entity
    {
        //public int Id { get; set; }
        public int TourId {  get; set; }
        public int UserId { get; set; }
        public string TourName { get; set; }

        public OrderItem() { }
        public OrderItem( int tourId, int userId, string tourName)
        {
            //Id = id;
            TourId = tourId;
            UserId = userId;
            TourName = tourName;
        }
    }
}