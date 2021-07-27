using API;
using Application.Interfaces;
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
    public class IndividualPromotionTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly IRepository repository;

        public IndividualPromotionTests()
        {
            this.repository = new Infrastructure.Repository(); ;
        }

        [Fact]
        public void CalculateTotal_IfPromotionIsValid_ReturnPromotionPrice()
        {
            var orderItemId = "A";

            var promotion = Promotions.GetPromotions().FirstOrDefault(p => p.ApplicableIDs.Contains(orderItemId));

            var order = new Order()
            {
                Items = new List<Item>()
                {
                    new Item() {Id = "A", UnitPrice = 50, OrderedAmount = 5},

                }
            };

            var strategy = new IndividualPromotionStrategy(promotion, order, orderItemId);
            var total = strategy.CalculateTotal();

            total.Should().Be(230);
        }
    }
}