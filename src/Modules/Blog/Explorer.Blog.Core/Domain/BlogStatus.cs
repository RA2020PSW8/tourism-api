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
        public string Name { get; init; }
        public int RequiredPoints { get; init; }

        public BlogStatus()
        {

        }

        public BlogStatus(string name, int requiredPoints)
        {
            Name = name;
            RequiredPoints = requiredPoints;
        }
    }
}
