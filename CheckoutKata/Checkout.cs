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
    }
}