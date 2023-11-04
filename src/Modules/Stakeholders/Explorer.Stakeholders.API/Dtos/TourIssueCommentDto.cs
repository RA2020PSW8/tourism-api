using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TourIssueCommentDto
    {
        public int Id { get; set; }
        public int TourID { get; init; }
        public int UserID { get; init; }
        public required string Comment { get; init; }
        public DateTime CreationDateTime { get; init; }
    }
}
