using Domain.Orders;
using System.Linq;

namespace Application.Strategies
{
    public class CombinedPromotionStrategy : IPromotionStrategy
    {
        private readonly Promotion promotion;
        private readonly Order order;
        private readonly bool combinedPromotionIsActive;

        public CombinedPromotionStrategy(Promotion promotion, Order order)
        {
            this.promotion = promotion;
            this.order = order;
        }

        public int CalculateTotal()
        {
            var applicableIDs = promotion.ApplicableIDs;
            var items = order.Items;

            if (!promotion.Active)
            {
                var foundItems = order.Items.Where(i => applicableIDs.Contains(i.Id));
                var minimumAmount = foundItems.Min(o => o.OrderedAmount);
                var maximumAmount = foundItems.Max(o => o.OrderedAmount);
                var highestOrderedAmount = order.Items.Where(i => applicableIDs.Contains(i.Id)).Aggregate((previous, next) =>
                    previous.OrderedAmount > next.OrderedAmount ? previous : next);

                if (foundItems.Count() > 1)
                {
                    if (maximumAmount == minimumAmount)
                    {
                        promotion.Active = true;
                        return promotion.DiscountedPrice * minimumAmount;
                    }

                    return (promotion.DiscountedPrice * minimumAmount) +
                           highestOrderedAmount.UnitPrice * (maximumAmount - minimumAmount);
                }

                return highestOrderedAmount.OrderedAmount * highestOrderedAmount.UnitPrice;
            }

            return 0;
        }
    }
}