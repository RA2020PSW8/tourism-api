using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class ShoppingCart: Entity
    {
        public int UserId { get; init; }
        public List<int> OrdersId { get; set; }
        public double Price { get; set; }

        public ShoppingCart()
        {
            OrdersId = new List<int>();
        }
        public ShoppingCart( int userId, List<int> ordersId, double price)
        {
            UserId = userId;
            OrdersId = ordersId;
            Price = price;
        }
    }
}
