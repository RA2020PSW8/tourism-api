using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourIssue : Entity
    {
        public required string Category { get; init; }
        public uint Priority { get; init; }
        public required string Description { get; init; }
        public DateTime DateTime { get; init; }

        public TourIssue()
        {

        }

        public TourIssue(string category, uint priority, string description, DateTime dateTime)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentNullException("Category must be filled.");
            if (priority < 1 || priority > 5) throw new ArgumentException("Invalid priority."); 
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException("Description is required.");

            Category = category;
            Priority = priority;
            Description = description;
            DateTime = dateTime;
        }
    }
}
