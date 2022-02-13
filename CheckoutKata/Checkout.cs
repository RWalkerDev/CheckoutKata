using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly IList<string> _basket;
        private readonly IDictionary<string, decimal> _stockKeepingUnits;

        public Checkout(IDictionary<string, decimal> stockKeepingUnits)
        {
            _basket = new List<string>();
            _stockKeepingUnits = stockKeepingUnits;
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
            return _basket.Sum(i => _stockKeepingUnits[i]);
        }
    }
}