using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly IList<string> _basket;
        private readonly IDictionary<string, decimal> _stockKeepingUnits;
        private readonly IPromotionsCalculator _promotionsCalculator;

        public Checkout(IDictionary<string, decimal> stockKeepingUnits, IPromotionsCalculator promotionsCalculator)
        {
            _basket = new List<string>();
            _stockKeepingUnits = stockKeepingUnits;
            _promotionsCalculator = promotionsCalculator;
        }

        public void Scan(string item)
        {
            _basket.Add(item);
        }

        public int TotalItems()
        {
            return _basket.Count;
        }

        public decimal TotalCost()
        {
            var totalCost = _basket.Sum(i => _stockKeepingUnits[i]);
            var items = _basket.GroupBy(x => x).ToDictionary(item => item.Key, count => count.Count());
            var discount = items.Sum(i => _promotionsCalculator.CalculateDiscount(i.Key, i.Value));

            return totalCost - discount;
        }
    }

    public class Promotion
    {
        public string StockKeepingUnitId { get; }
        public int QuantityRequiredForPromotion { get; }
        public decimal Discount { get; }

        public Promotion(string stockKeepingUnitId, int quantityRequiredForPromotion, decimal discount)
        {
            StockKeepingUnitId = stockKeepingUnitId;
            QuantityRequiredForPromotion = quantityRequiredForPromotion;
            Discount = discount;
        }
    }

    public interface IPromotionsCalculator
    {
        decimal CalculateDiscount(string skuId, int count);
    }

    public class PromotionsCalculator : IPromotionsCalculator
    {
        private readonly IEnumerable<Promotion> _promotions;

        public PromotionsCalculator(IEnumerable<Promotion> promotions)
        {
            _promotions = promotions ?? throw new ArgumentNullException(nameof(promotions));
        }

        public decimal CalculateDiscount(string skuId, int count)
        {
            var promotion = _promotions.FirstOrDefault(i => i?.StockKeepingUnitId == skuId);
            if (promotion == null)
                return 0;

            var timesToApplyDiscount = count / promotion.QuantityRequiredForPromotion;
            return promotion.Discount * timesToApplyDiscount;
        }
    }
}