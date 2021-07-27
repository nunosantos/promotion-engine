using System.Collections.Generic;

namespace Domain.Orders
{
    public class Order
    {
        public List<Item> Items { get; set; }
        public int Total { get; set; }
    }
}
