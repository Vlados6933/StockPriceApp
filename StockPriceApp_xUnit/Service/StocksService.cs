using Entities;
using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO;
using System.ComponentModel.DataAnnotations;

namespace Service
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
        }
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if(buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            if(buyOrderRequest.DateAndTimeOfOrder < DateTime.Parse("2000-01-01"))
            {
                throw new ArgumentException(nameof(buyOrderRequest.DateAndTimeOfOrder));
            }

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            _buyOrders.Add(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if(sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));

            ValidationHelper.ModelValidation(sellOrderRequest);

            if (sellOrderRequest.DateAndTimeOfOrder < DateTime.Parse("2000-01-01"))
            {
                throw new ArgumentException(nameof(sellOrderRequest.DateAndTimeOfOrder));
            }

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            _sellOrders.Add(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            return _buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            return _sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}
