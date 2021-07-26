using Application.Interfaces;
using Domain.Orders;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Unit_tests.Repository
{
    [TestCaseOrderer("Unit_tests.Config.AlphabeticalOrderer", "Unit_tests.Repository")]
    public class RepositoryTests
    {
        private readonly IRepository _repository;


        public RepositoryTests()
        {
            _repository = new Infrastructure.Repository();
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
            _repository.Add(
                new List<Item>()
                {
                    new Item()
                    {
                        Id = 'A' ,
                        UnitPrice = 20
                    },
                    new Item()
                    {
                        Id = 'B' ,
                        UnitPrice = 20
                    }
                });

            var item = _repository.Get();
            item.Should().HaveCount(2);
        }
    }

   
}
