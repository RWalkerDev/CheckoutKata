namespace CheckoutKata
{
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