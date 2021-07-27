using Domain.Orders;
using System.Linq;

namespace Application.Strategies
{
    public class PromotionNotActiveStrategy : IPromotionStrategy
    {
        private readonly Order order;
        private readonly string skuId;

        public PromotionNotActiveStrategy(Order order, string skuId)
        {
            this.order = order;
            this.skuId = skuId;
        }

        public int CalculateTotal()
        {
            var orderItem = order.Items.FirstOrDefault(i => i.Id == skuId);
            return orderItem.OrderedAmount * orderItem.UnitPrice;
        }
    }
}