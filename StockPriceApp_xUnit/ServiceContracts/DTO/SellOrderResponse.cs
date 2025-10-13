using Entities;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(SellOrderResponse)) return false;

            SellOrderResponse sellOrder = (SellOrderResponse)obj;

            return SellOrderID == sellOrder.SellOrderID && StockSymbol == sellOrder.StockSymbol && StockName == sellOrder.StockName && DateAndTimeOfOrder == sellOrder.DateAndTimeOfOrder && Quantity == sellOrder.Quantity && Price == sellOrder.Price;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Sell Order ID: {SellOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, Date And Time Of Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy")}, Quantity: {Quantity}, Price: {Price}";
        }
    }
    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse
            {
                SellOrderID = sellOrder.SellOrderID,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                StockName = sellOrder.StockName,
                StockSymbol = sellOrder.StockSymbol
            };
        }
    }
}
