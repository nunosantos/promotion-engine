﻿using Domain.Orders;
using System.Collections.Generic;
using System.Linq;

namespace Application.Strategies
{
    public class PromotionNotActiveStrategy : IPromotionStrategy
    {
        private readonly Order order;
        private readonly string skuId;
        private readonly IEnumerable<Product> products;

        public PromotionNotActiveStrategy(Order order, string skuId, IEnumerable<Product> products)
        {
            this.order = order;
            this.skuId = skuId;
            this.products = products;
        }

        public int CalculateTotal()
        {
            var product = products.FirstOrDefault(i => i.Id == skuId);
            var orderItem = order.OrderItems.FirstOrDefault(i => i.Id == skuId);
            return orderItem.OrderedAmount * product.UnitPrice;
        }
    }
}