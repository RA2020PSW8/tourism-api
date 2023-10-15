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
        public enum BlogStatus{ DRAFT, PUBLISHED, CLOSED }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public DateTime CreationDate { get; init; }
        public List<string>? ImageLinks { get; init; }
        public BlogStatus Status { get; init; }

        public Blog()
        {

        }

        public Blog(string title, string description, DateTime creationDate, List<string>? imageLinks, BlogStatus status)
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
