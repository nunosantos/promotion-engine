using API;
using Application.Interfaces;
using Domain.Orders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using Xunit;

namespace Unit_tests.OrderCalculator
{
    public class OrderCalculatorTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly IRepository _repository;

        public OrderCalculatorTests()
        {
            _repository = new Infrastructure.Repository();
        }

        [Fact]
        public void CreateOrder_ScenarioA_Buying_1xA_1xB_1xC_Returns100()
        {
            var order = new Order()
            {
                Items = new List<Item>()
                {
                    new Item() { Id ="A", UnitPrice = 50, OrderedAmount = 1},
                    new Item() { Id ="B", UnitPrice = 30, OrderedAmount = 1},
                    new Item() { Id ="C", UnitPrice = 20, OrderedAmount = 1 }
                }
            };

            var cartCalculator = new Application.Services.OrderCalculator(_repository);

            var totalCalculatedCost = cartCalculator.CalculateItemTotal(order);

            totalCalculatedCost.Should().Be(100);
        }

        [Fact]
        public void CreateOrder_ScenarioB_Buying_5xA_5xB_1xC_Returns370()
        {
            var order = new Order()
            {
                Items = new List<Item>()
                {
                    new Item() { Id ="A", UnitPrice = 50, OrderedAmount = 5},
                    new Item() { Id ="B", UnitPrice = 30, OrderedAmount = 5},
                    new Item() { Id ="C", UnitPrice = 20, OrderedAmount = 1}
                }
            };

            var cartCalculator = new Application.Services.OrderCalculator(_repository);

            var totalCalculatedCost = cartCalculator.CalculateItemTotal(order);

            totalCalculatedCost.Should().Be(370);
        }

        [Fact]
        public void CreateOrder_ScenarioC_Buying_3xA_5xB_1xC_1xD_Returns280()
        {
            var order = new Order()
            {
                Items = new List<Item>()
                {
                    new Item() { Id ="A", UnitPrice = 50, OrderedAmount = 3},
                    new Item() { Id ="B", UnitPrice = 30, OrderedAmount = 5},
                    new Item() { Id ="C", UnitPrice = 20, OrderedAmount = 1},
                    new Item() { Id ="D", UnitPrice = 20, OrderedAmount = 1}
                }
            };

            var cartCalculator = new Application.Services.OrderCalculator(_repository);

            var totalCalculatedCost = cartCalculator.CalculateItemTotal(order);

            totalCalculatedCost.Should().Be(280);
        }

        [Fact]
        public void CreateOrder_ScenarioD_AddNewPromotion_1xE_Returns300()
        {
            var order = new Order()
            {
                Items = new List<Item>()
                {
                    new Item() { Id ="A", UnitPrice = 50, OrderedAmount = 3},
                    new Item() { Id ="B", UnitPrice = 30, OrderedAmount = 5},
                    new Item() { Id ="C", UnitPrice = 20, OrderedAmount = 1},
                    new Item() { Id ="D", UnitPrice = 20, OrderedAmount = 1},
                    new Item() { Id ="E", UnitPrice = 20, OrderedAmount = 1}
                }
            };

            var cartCalculator = new Application.Services.OrderCalculator(_repository);

            var totalCalculatedCost = cartCalculator.CalculateItemTotal(order);

            totalCalculatedCost.Should().Be(300);
        }

        [Fact]
        public void CreateOrder_ScenarioE_AddNewPromotion_1xE_4xF_Returns380()
        {
            var order = new Order()
            {
                Items = new List<Item>()
                {
                    new Item() { Id ="A", UnitPrice = 50, OrderedAmount = 3},
                    new Item() { Id ="B", UnitPrice = 30, OrderedAmount = 5},
                    new Item() { Id ="C", UnitPrice = 20, OrderedAmount = 1},
                    new Item() { Id ="D", UnitPrice = 20, OrderedAmount = 1},
                    new Item() { Id ="E", UnitPrice = 20, OrderedAmount = 1},
                    new Item() { Id ="F", UnitPrice = 20, OrderedAmount = 4},
                }
            };

            var cartCalculator = new Application.Services.OrderCalculator(_repository);

            var totalCalculatedCost = cartCalculator.CalculateItemTotal(order);

            totalCalculatedCost.Should().Be(380);
        }
    }
}