using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly IList<string> _basket;

        public Checkout()
        {
            _basket = new List<string>();
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
            var items = new Dictionary<string, decimal>
            {
                {"A", 10},
                {"B", 15},
                {"C", 40},
                {"D", 55}
            };

            return _basket.Sum(i => items[i]);
        }
    }
}