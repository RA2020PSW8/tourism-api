using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class ForumComment : Entity
    {
        public int ForumId { get; private set; }
        public User? User { get; private set; }  
        public string? Comment { get; private set; }
        public DateTime PostTime { get; private set; }  
        public DateTime LastEditTime { get; set; }
        public bool IsDeleted { get; set; }

        public ForumComment() 
        {
            Id = -1;
        }

        public ForumComment(int forumId, User? user, string? comment)
        {
            Id = -1;
            ForumId = forumId;
            User = user;
            Comment = string.IsNullOrEmpty(comment) ? string.Empty : comment;
            PostTime = DateTime.Now;
        }
    }
}
