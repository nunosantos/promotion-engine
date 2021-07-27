using Domain.Orders;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Domain_tests
{
    public class DomainPromotionTest
    {
        [Fact]
        public void CreatesPromotion_WhenSKUIDIsA_Returns50()
        {
            var orderItems = new Promotion()
            {
                Id = 1,
                UnitCostAmount = 30,
                Items = Enumerable.Repeat(new Item(), 3),
                PriceTrigger = 3
            };

            orderItems.Id.Should().Be(1);
            orderItems.UnitCostAmount.Should().Be(30);
            orderItems.Items.Should().HaveCount(3);
            orderItems.PriceTrigger.Should().Be(3);
        }
    }
}
