using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly IList<string> _basket;
        private readonly IStockKeepingUnitRepository _stockKeepingUnitRepository;
        private readonly IPromotionsCalculator _promotionsCalculator;

        public Checkout(IStockKeepingUnitRepository stockKeepingUnitRepository,
            IPromotionsCalculator promotionsCalculator)
        {
            _basket = new List<string>();
            _stockKeepingUnitRepository = stockKeepingUnitRepository;
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
            var allItems = _basket.Select(i => _stockKeepingUnitRepository.GetById(i));
            var totalCost = allItems.Sum(i => i.UnitPrice);
            var discount = _promotionsCalculator.CalculateDiscount(allItems);

            return totalCost - discount;
        }
    }
}