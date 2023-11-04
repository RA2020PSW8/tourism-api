using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class TourIssue : Entity
    {
        public required string Category { get; init; }
        public uint Priority { get; init; }
        public required string Description { get; init; }
        public DateTime CreationDateTime { get; init; }
        public DateTime? ResolveDateTime { get; private set; }
        public bool IsResolved {  get; private set; }


        public TourIssue()
        {

        }

        public void Resolve()
        {
            IsResolved = true;
            ResolveDateTime = DateTime.Now;
        }

        public void UndoResolve()
        {
            IsResolved = true;
            ResolveDateTime = null;
        }

        public TourIssue(string category, uint priority, string description, DateTime creationDateTime, DateTime resolveDateTime, bool isResolved)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentNullException("Category must be filled.");
            if (priority < 0 || priority > 5) throw new ArgumentException("Invalid priority."); 
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException("Description is required.");

            Category = category;
            Priority = priority;
            Description = description;
            CreationDateTime = creationDateTime;
            ResolveDateTime = resolveDateTime;
            IsResolved = isResolved;
        }
    }
}
