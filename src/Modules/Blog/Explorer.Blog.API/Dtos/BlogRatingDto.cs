using Explorer.Blog.API.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogRatingDto
    {
        public long BlogId { get; set; } 
        public long UserId { get; set; }
        public string Username { get; set; }
        public DateTime CreationTime { get; set; } 
        public string Rating { get; set; }
    }
}
