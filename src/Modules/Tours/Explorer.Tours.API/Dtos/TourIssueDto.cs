using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourIssueDto
    {
        public int Id { get; set; }
        public required string Category { get; set; }
        public uint Priority { get; set; }
        public required string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
