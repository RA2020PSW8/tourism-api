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
        public string? Username { get; private set; }  
        public string Comment { get; private set; }
        public DateTime PostTime { get; private set; }  
        public DateTime LastEditTime { get; private set; }
        public bool IsDeleted { get; private set; }

        public ForumComment()
        {

        }

        public ForumComment(int forumId, string? username, string comment,DateTime postTime,DateTime lastEditTime,bool isDeleted)
        {
            ForumId = forumId;
            Username = username;
            if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("Invalid comment");
            
            Comment = comment;
            PostTime = postTime;
            LastEditTime = lastEditTime;
            IsDeleted = isDeleted;
        }
    }
}
