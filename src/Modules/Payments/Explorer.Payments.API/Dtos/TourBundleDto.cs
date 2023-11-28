using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class TourBundleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
