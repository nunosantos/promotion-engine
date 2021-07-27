using Application.Interfaces;
using Application.Strategies;
using Application.Stubs;
using Domain.Orders;
using System;
using System.Linq;

namespace Application.Services
{
    public class OrderCalculator
    {
        private readonly IRepository _promotionRepository;

        public OrderCalculator(IRepository promotionRepository)
        {
            this._promotionRepository = promotionRepository;
        }

        public int CalculateItemTotal(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            var total = 0;
            var promotionCalculator = new PromotionCalculationContext();
            var promotions = Promotions.GetPromotions().ToList();

            foreach (var orderItem in order.Items)
            {
                var promotion = promotions
                                        .FirstOrDefault(p =>
                                            p.ApplicableIDs.Contains(orderItem.Id));

                switch (promotion?.ApplicableIDs.Length)
                {
                    case 1:
                        promotionCalculator.SetPromotionStrategy(new IndividualPromotionStrategy(promotions.FirstOrDefault(i => i.Id == orderItem.Id), order, orderItem.Id));
                        total += promotionCalculator.CalculateTotal();
                        break;

                    case > 1:
                        promotionCalculator.SetPromotionStrategy(new CombinedPromotionStrategy(promotion, order));
                        total += promotionCalculator.CalculateTotal();
                        break;

                    default:
                        promotionCalculator.SetPromotionStrategy(new PromotionNotActiveStrategy(order, orderItem.Id));
                        total += promotionCalculator.CalculateTotal();
                        break;
                }
            }

            return total;
        }
    }
}