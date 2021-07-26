using Application.Interfaces;
using Domain.Orders;
using System.Collections.Generic;

namespace Infrastructure
{
    public class Repository : IRepository
    {
        private readonly List<Item> _items;

        public Repository()
        {
            _items = new List<Item>();
        }

        public void Add(IEnumerable<Item> items)
        {
            _items.AddRange(items);
        }

        public IEnumerable<Item> Get()
        {
            return _items;
        }
    }
}