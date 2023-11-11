using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class BlogRating : ValueObject<BlogRating>
    {
        public long UserId { get; private set; }
        public DateTime CreationTime { get; private set; }

        protected override bool EqualsCore(BlogRating other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
