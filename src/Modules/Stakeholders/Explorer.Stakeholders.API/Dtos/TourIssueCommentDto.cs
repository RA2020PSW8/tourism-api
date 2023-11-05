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
        public int TourIssueId { get; set; }
        public int UserId { get; set; }
        public required string Comment { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
