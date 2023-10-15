using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogDto
    {
        public enum BlogStatus { DRAFT, PUBLISHED, CLOSED }
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string>? ImageLinks { get; set; }
        public BlogStatus Status { get; set; }
    }
}
