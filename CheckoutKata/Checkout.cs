using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Checkout
    {
        private IEnumerable<string> _basket;

        public Checkout()
        {
            _basket = new List<string>();
        }

        public void Scan(string item)
        {
            throw new System.NotImplementedException();
        }

        public int TotalItems()
        {
            return _basket.Count();
        }
    }
}