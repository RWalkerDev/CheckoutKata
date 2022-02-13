namespace CheckoutKata
{
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