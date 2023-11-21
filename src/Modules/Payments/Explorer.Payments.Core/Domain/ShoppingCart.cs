using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

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