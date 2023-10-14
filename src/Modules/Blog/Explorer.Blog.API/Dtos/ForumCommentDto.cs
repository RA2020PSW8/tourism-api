using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class ForumCommentDto
    {
        public int ForumId { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
    }
}
