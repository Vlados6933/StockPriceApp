using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class SellOrderRequest
    {
        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string? StockSymbol { get; set; }
        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares in single order. Minimum is 1.")]
        public uint Quantity { get; set; }
        [Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minimum is 1")]
        public double Price { get; set; }


        public SellOrder ToSellOrder()
        {
            return new SellOrder
            {
                StockName = StockName,
                Price = Price,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                StockSymbol = StockSymbol
            };
        }
    }
}
