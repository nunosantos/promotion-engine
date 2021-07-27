using API;
using Application.Strategies;
using Application.Stubs;
using Domain.Orders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Unit_tests.Strategies
{
    public class CombinedPromotionTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly Infrastructure.Repository _repository;

        public CombinedPromotionTests()
        {
            _repository = new Infrastructure.Repository();
        }

        [Fact]
        public void CalculateTotal_IfItemsSharePromotion_ReturnPromotionPrice()
        {
            var orderItemId = "C";

            var promotion = Promotions.GetPromotions().FirstOrDefault(p => p.ApplicableIDs.Contains(orderItemId));

            var order = new Order()
            {
                Items = new List<Item>()
                {
                    new Item() { Id ="C", UnitPrice = 30, OrderedAmount = 1 },
                    new Item() { Id ="D", UnitPrice = 20, OrderedAmount = 1 }
                }
            };

            var strategy = new CombinedPromotionStrategy(promotion, order);
            var total = strategy.CalculateTotal();

            total.Should().Be(30);
        }

        [Fact]
        public void CalculateTotal_If_Promotion_AlreadyActive_Return60()
        {
            var orderItemId = "C";

            var promotion = Promotions.GetPromotions().FirstOrDefault(p => p.ApplicableIDs.Contains(orderItemId));

            var order = new Order()
            {
                Items = new List<Item>()
                {
                    new Item() { Id ="C", UnitPrice = 30, OrderedAmount = 2 },
                    new Item() { Id ="D", UnitPrice = 20, OrderedAmount = 2 }
                }
            };

            var strategy = new CombinedPromotionStrategy(promotion, order);
            var total = strategy.CalculateTotal();

            total.Should().Be(60);
        }

        [Fact]
        public void CalculateTotal_Return45()
        {
            var orderItemId = "C";

            var promotion = Promotions.GetPromotions().FirstOrDefault(p => p.ApplicableIDs.Contains(orderItemId));

            var order = new Order()
            {
                Items = new List<Item>()
                {
                    new Item() { Id ="C", UnitPrice = 20, OrderedAmount = 1 },
                    new Item() { Id ="D", UnitPrice = 15, OrderedAmount = 2 }
                }
            };

            var strategy = new CombinedPromotionStrategy(promotion, order);
            var total = strategy.CalculateTotal();

            total.Should().Be(45);
        }
    }
}