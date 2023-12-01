using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Bundle : Entity
    {
        public string Name { get; init; }
        public double TotalPrice { get; init; }
        public string Status { get; init; }
        public ICollection<Tour> Tours { get; set; }   

        public Bundle()
        {
            Tours = new List<Tour>();
        }

        public Bundle(long id, string name, string status)
        {
            Id = id;
            Name = name;
            
            Status = status;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new ArgumentException("Invalid Name");
           
            if (string.IsNullOrEmpty(Status)) throw new ArgumentException("Invalid status");

        }
    }
}
