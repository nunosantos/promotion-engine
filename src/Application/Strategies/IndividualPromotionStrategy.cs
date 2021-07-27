﻿using Domain.Orders;
using System;
using System.Linq;

namespace Application.Strategies
{
    public class IndividualPromotionStrategy : IPromotionStrategy
    {
        private readonly Promotion promotion;
        private readonly Order order;
        private readonly string skuId;

        public IndividualPromotionStrategy(Promotion promotion, Order order, string skuId)
        {
            this.promotion = promotion;
            this.order = order;
            this.skuId = skuId;
        }

        public int CalculateTotal()
        {
            if (promotion is null)
            {
                throw new ArgumentNullException(nameof(promotion));
            }

            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var matchedOrderItem = order.Items.FirstOrDefault(i => i.Id == skuId);

            if (matchedOrderItem?.OrderedAmount < promotion.PriceTrigger) return matchedOrderItem.OrderedAmount * matchedOrderItem.UnitPrice;
            {
                var total = 0;

                var numberOfItemsYetToBeCalculated = 0;

                for (var i = 0; i <= matchedOrderItem.OrderedAmount; i++)
                {
                    if (i % promotion.PriceTrigger != 0 || i == 0) continue;

                    total += promotion.DiscountedPrice;

                    numberOfItemsYetToBeCalculated = matchedOrderItem.OrderedAmount - i;
                }

                return total += matchedOrderItem.UnitPrice * numberOfItemsYetToBeCalculated;
            }

        }
    }
}