using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RequiredPoints { get; set; }
    }
}
