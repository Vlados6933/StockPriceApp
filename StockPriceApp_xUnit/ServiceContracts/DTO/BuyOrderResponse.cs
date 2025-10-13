using Entities;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(BuyOrderResponse)) return false;

            BuyOrderResponse buyOrder = (BuyOrderResponse)obj;

            return BuyOrderID == buyOrder.BuyOrderID && StockSymbol == buyOrder.StockSymbol && StockName == buyOrder.StockName && DateAndTimeOfOrder == buyOrder.DateAndTimeOfOrder && Quantity == buyOrder.Quantity && Price == buyOrder.Price;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Buy Order ID: {BuyOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, Date And Time Of Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy")}, Quantity: {Quantity}, Price: {Price}";
        }
    }

    public static class BuyOrderExtensions
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse
            {
                BuyOrderID = buyOrder.BuyOrderID,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price,
                StockName = buyOrder.StockName,
                StockSymbol = buyOrder.StockSymbol
            };
        }
    }
}
