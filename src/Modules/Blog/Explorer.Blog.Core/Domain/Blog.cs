using Explorer.Blog.Core.Domain.Enum;
using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class Blog : Entity
    {
        public int CreatorId { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public Enum.BlogStatus Status { get; init; }
        public DateOnly CreationDate { get; init; }
        public List<string>? ImageLinks { get; init; }

        public Blog()
        {

        }

        public Blog(string title, string description, DateOnly creationDate, List<string> imageLinks, Enum.BlogStatus status)
        {
            if(string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.");

            Title = title;
            Description = description;
            CreationDate = creationDate;
            ImageLinks = imageLinks;
            Status = status;
        }
    }
}
