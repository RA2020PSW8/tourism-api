using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> OrdersId {  get; set; }
        public double Price { get; set; }
    }
}
