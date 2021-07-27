using System.Linq;
using Domain.Orders;

namespace Application.Services
{
    public class OrderCalculator
    {
        public int CalculateTotal(Order order)
        {
            var combinedPromotionIsActive = false;
            var total = 0;
            var skuIds = order.Items.Select(i => i.Id).ToList();
            var orderItems = order.Items.ToList();

            foreach (var skuId in skuIds)
            {
                //group individual promotions
                if (skuId == "A")
                {
                    var promotionUnitCost = 130;
                    var numberOfTimesPromotionIsValid = 0;
                    var itemsYetToBeCalculated = 0;
                    var orderItem = orderItems.FirstOrDefault(i => i.Id == skuId);
                    for (var i = 1; i <= orderItem.Amount; i++)
                    {
                        if (i % 3 == 0 && orderItem.Amount >= i)
                        {
                            numberOfTimesPromotionIsValid++;
                            itemsYetToBeCalculated = orderItem.Amount - i;
                        }
                    }

                    if (numberOfTimesPromotionIsValid > 0)
                    {
                        total += (promotionUnitCost * numberOfTimesPromotionIsValid) +
                                 (itemsYetToBeCalculated * orderItem.UnitPrice);
                    }
                    else
                    {
                        total += orderItem.Amount * orderItem.UnitPrice;
                    }


                    continue;
                }
                else if (skuId == "B")
                {
                    var promotionUnitCost = 45;
                    var numberOfTimesPromotionIsValid = 0;
                    var itemsYetToBeCalculated = 0;
                    var orderItem = order.Items.FirstOrDefault(i => i.Id == skuId);
                    for (var i = 1; i <= orderItem.Amount; i++)
                    {
                        if (i % 2 == 0 && orderItem.Amount >= i)
                        {
                            numberOfTimesPromotionIsValid++;
                            itemsYetToBeCalculated = orderItem.Amount - i;
                        }
                    }

                    if (numberOfTimesPromotionIsValid > 0)
                    {
                        total += (promotionUnitCost * numberOfTimesPromotionIsValid) +
                                 (itemsYetToBeCalculated * orderItem.UnitPrice);
                    }
                    else
                    {
                        total += orderItem.Amount * orderItem.UnitPrice;
                    }

                    continue;
                }
                else if (skuId == "C" ^ skuId == "D")
                {

                    if (!combinedPromotionIsActive)
                    {
                        if (orderItems.Any(i => i.Id == "D"))
                        {
                            combinedPromotionIsActive = true;
                            continue;
                        }

                        var item = orderItems.FirstOrDefault(i => i.Id == skuId);
                        total += item.Amount * item.UnitPrice;
                        continue;
                    }

                    total += 30;
                }
                else
                //group combined promotions
                {
                    var item = orderItems.FirstOrDefault(i => i.Id == skuId);
                    total += item.Amount * item.UnitPrice;
                }
            }

            return total;
        }
    }
}
