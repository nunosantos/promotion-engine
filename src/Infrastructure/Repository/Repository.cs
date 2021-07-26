using Application.Interfaces;
using Domain.Orders;
using System.Collections.Generic;

namespace Infrastructure
{
    public class Repository : IRepository
    {
        private IEnumerable<Item> _items;

        public void Add(IEnumerable<Item> item)
        {
            _items = item;
        }
    }
}
