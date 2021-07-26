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
    }
}
