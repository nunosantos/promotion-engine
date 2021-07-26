using Application.Interfaces;
using Domain.Orders;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xunit;

namespace Unit_tests.Repository
{
    public class RepositoryTests
    {
        private readonly IRepository _repository;


        public RepositoryTests(IRepository repository)
        {
            _repository = repository;
        }

        [Fact]
        public void Add_Item_To_Repository()
        {
            _repository.Add(
                new List<Item>()
                {
                    new Item()
                    {
                        Id = 'A' ,
                        UnitPrice = 20
                    }
                });
        }

        [Fact]
        public void Get_Items_From_Repository()
        {
            var item = _repository.Get();
        }
    }
}
