using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class BlogStatus : Entity
    {
        public long BlogId { get; init; }
        public string Name { get; init; }

        public BlogStatus()
        {

        }

        public BlogStatus(long blogId,string name)
        {
            BlogId = blogId;
            Name = name;

            if (name == "")
            {
                throw new ArgumentException("Name cannot be empty");
            }
        }
    }
}
