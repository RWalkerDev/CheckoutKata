using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly IList<string> _basket;
        private readonly IDictionary<string, decimal> _stockKeepingUnits;
        private readonly IEnumerable<Promotion> _promotions;

        public Checkout(IDictionary<string, decimal> stockKeepingUnits, IEnumerable<Promotion> promotions)
        {
            _basket = new List<string>();
            _stockKeepingUnits = stockKeepingUnits;
            _promotions = promotions;
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
            decimal discount = 0;
            foreach (var promotion in _promotions)
            {
                var items = _basket.Where(i => i == promotion.StockKeepingUnitId);

                var timesToApplyDiscount = items.Count() / promotion.QuantityRequiredForPromotion;
                discount += promotion.Discount * timesToApplyDiscount;
            }

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
}