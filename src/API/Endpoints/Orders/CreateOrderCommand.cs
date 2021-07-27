using System.Collections.Generic;

namespace API.Endpoints.Orders
{
    public class CreateOrderCommand
    {
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public string Id { get; set; }
        public int Amount { get; set; }
    }
}
