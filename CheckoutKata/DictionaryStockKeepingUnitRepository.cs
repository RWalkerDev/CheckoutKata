using System.Collections.Generic;

namespace CheckoutKata
{
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

    //Would have this interface in it's own file, but for brevity of this kata
    public interface IStockKeepingUnitRepository
    {
        StockKeepingUnit GetById(string id);
    }
}