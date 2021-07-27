using Domain.Orders;
using System.Collections.Generic;
using System.Linq;

namespace Application.Strategies
{
    public class CombinedPromotionStrategy : IPromotionStrategy
    {
        private readonly Promotion promotion;
        private readonly Order order;
        private readonly IEnumerable<Product> products;
        private readonly bool combinedPromotionIsActive;

        public CombinedPromotionStrategy(Promotion promotion, Order order, IEnumerable<Product> products)
        {
            this.promotion = promotion;
            this.order = order;
            this.products = products;
        }

        public int CalculateTotal()
        {
            var applicableIDs = promotion.ApplicableIDs;
            //var products = order.OrderItems;

            if (!promotion.Active)
            {
                var foundItems = order.OrderItems.Where(i => applicableIDs.Contains(i.Id));
                var minimumAmount = foundItems.Min(o => o.OrderedAmount);
                var maximumAmount = foundItems.Max(o => o.OrderedAmount);
                var highestOrderedAmount = foundItems.Aggregate((previous, next) =>
                    previous.OrderedAmount > next.OrderedAmount ? previous : next);

                var matchedProduct = products.FirstOrDefault(p => p.Id == highestOrderedAmount.Id);

                if (foundItems.Count() > 1)
                {
                    if (maximumAmount == minimumAmount)
                    {
                        promotion.Active = true;
                        return promotion.DiscountedPrice * minimumAmount;
                    }

                    return (promotion.DiscountedPrice * minimumAmount) +
                           matchedProduct.UnitPrice * (maximumAmount - minimumAmount);
                }

                return highestOrderedAmount.OrderedAmount * matchedProduct.UnitPrice;
            }

            return 0;
        }
    }
}