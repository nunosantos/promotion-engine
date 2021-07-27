using API.Endpoints.Orders;
using Domain.Orders;
using System.Collections.Generic;
using System.Linq;

namespace API.Endpoints.Items
{
    public class RepositoryMapper
    {
        public static IEnumerable<Item> MapItem(CreateItemCommand request)
        {
            return request
                        .Items
                        .Select(i => new Item { Id = i.Id, UnitPrice = i.UnitPrice });
        }

        public static Order MapOrder(CreateOrderCommand request)
        {
            return new Order()
            {
                Items = request.OrderItems.Select(i =>
                new Item
                {
                    Amount = i.Amount,
                    Id = i.Id,
                    UnitPrice = i.
                }).ToList()
            };
        }
    }
}
