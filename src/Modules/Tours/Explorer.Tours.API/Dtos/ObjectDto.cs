using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ObjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string Category { get; set; }


        public ObjectDto(int id, string name, string? description, string? image, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Category = category;
        }
    }
}
