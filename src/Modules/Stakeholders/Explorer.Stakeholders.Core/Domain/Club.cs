using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
  

    public class Club: Entity 
    {
        public string Name { get; init; }
        public string ?Description { get; init; }

        public string ?Image { get; init; }

        public int UserId { get; init; }

        public Club(string name, string? description, string? image, int userId)
        {

            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("Invalid name.");
          

            Name = name; 
            Description = description;
            Image = image;  
            UserId = userId;
        }


    }
}
