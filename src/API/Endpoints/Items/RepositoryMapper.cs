using API.Endpoints.Orders;
using Domain.Orders;
using System.Collections.Generic;
using System.Linq;

namespace API.Endpoints.Items
{
    public class RepositoryMapper
    {
        public static IEnumerable<Product> MapItem(CreateItemCommand request, Application.Interfaces.IRepository _repository)
        {
            return request
                        .Items
                        .Select(i => new Product { Id = i.Id, UnitPrice = i.UnitPrice });
        }

        public static Order MapOrder(CreateOrderCommand request, IEnumerable<Product> existingItems)
        {
            return new Order()
            {
                OrderItems = request.OrderItems
                //OrderItems = request.OrderItems.Select(i =>
                //new Product
                //{
                //    OrderedAmount = i.OrderedAmount,
                //    Id = i.Id
                //}).ToList()
            };
        }
    }
}