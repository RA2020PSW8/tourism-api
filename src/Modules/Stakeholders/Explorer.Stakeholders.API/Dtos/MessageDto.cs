using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class MessageDto
    {
        public long ReceiverId { get; set; }
        public String Content { get; set; }
    }
}
