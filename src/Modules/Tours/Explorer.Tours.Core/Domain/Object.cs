using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Object : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string? Image { get; init; }
        public Category Category { get; init; }
        public ObjectStatus Status { get; init; }

        public Object() 
        {

        }

        public Object(string name, string description, string? image, Category category, ObjectStatus status)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            Image = image;
            Category = category;
            Status = status;
        }



    }
}
