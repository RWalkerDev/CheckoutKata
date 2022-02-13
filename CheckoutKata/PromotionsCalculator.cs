using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class PromotionsCalculator : IPromotionsCalculator
    {
        private readonly IEnumerable<Promotion> _promotions;

        public PromotionsCalculator(IEnumerable<Promotion> promotions)
        {
            _promotions = promotions ?? throw new ArgumentNullException(nameof(promotions));
        }

        public decimal CalculateDiscount(IEnumerable<StockKeepingUnit> basketItems)
        {
            var allItemsGrouped = basketItems.GroupBy(x => x.Id).ToDictionary(item => item.Key, count => count.Count());


            return allItemsGrouped.Sum(i =>
            {
                var (skuId, count) = i;

                var promotion = _promotions.FirstOrDefault(j => j.StockKeepingUnitId == skuId);
                if (promotion == null)
                    return 0;

                var timesToApplyDiscount = count / promotion.QuantityRequiredForPromotion;
                return promotion.Discount * timesToApplyDiscount;
            });
        }
    }

    //Would have this interface in it's own file, but for brevity of this kata
    public interface IPromotionsCalculator
    {
        decimal CalculateDiscount(IEnumerable<StockKeepingUnit> basketItems);
    }
}