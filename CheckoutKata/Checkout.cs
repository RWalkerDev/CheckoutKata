using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly IList<string> _basket;
        private readonly IStockKeepingUnitRepository _stockKeepingUnitRepository;
        private readonly IPromotionsCalculator _promotionsCalculator;

        public Checkout(IStockKeepingUnitRepository stockKeepingUnitRepository, IPromotionsCalculator promotionsCalculator)
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
            var totalCost = allItems.Select(i => i.UnitPrice).Sum();

            var allItemsGrouped = allItems.GroupBy(x => x.Id).ToDictionary(item => item.Key, count => count.Count());
            var discount = allItemsGrouped.Sum(i => _promotionsCalculator.CalculateDiscount(i.Key, i.Value));

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

    public interface IStockKeepingUnitRepository
    {
        StockKeepingUnit GetById(string id);
    }
    
    public class DictionaryStockKeepingUnitRepository : IStockKeepingUnitRepository
    {
        private readonly Dictionary<string, StockKeepingUnit> _stockKeepingUnits =
            new()
            {
                {"A", new StockKeepingUnit("A", 10)},
                {"B", new StockKeepingUnit("B", 15)},
                {"C", new StockKeepingUnit("C", 40)},
                {"D", new StockKeepingUnit("D", 55)}
            };

        public StockKeepingUnit GetById(string id)
        {
            return _stockKeepingUnits[id];
        }
    }

    public class StockKeepingUnit
    {
        public StockKeepingUnit(string id, decimal unitPrice)
        {
            Id = id;
            UnitPrice = unitPrice;
        }

        public string Id { get; }
        public decimal UnitPrice { get; }
    }
}
