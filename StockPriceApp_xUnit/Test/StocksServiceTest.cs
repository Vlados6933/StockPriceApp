using Service;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Test
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;

        public StocksServiceTest()
        {
            _stocksService = new StocksService();
        }

        #region CreateBuyOrder

        [Fact]
        public void CreateBuyOrder_BuyOrderRequestIsNull()
        {
            BuyOrderRequest? request = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public void CreateBuyOrder_BuyOrderQuantityIsZero()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 0,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 45,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public void CreateBuyOrder_BuyOrderQuantityIsOutOfRange()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100001,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 45,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public void CreateBuyOrder_BuyOrderPriceIsZero()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 0,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }


        [Fact]
        public void CreateBuyOrder_BuyOrderPriceIsOutOfRange()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 10001,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public void CreateBuyOrder_StockSymbolISNull()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 10,
                StockName = "dfs",
                StockSymbol = null
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public void CreateBuyOrder_DateAndTimeOfOrderIsOutOfRange()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31"),
                Price = 10,
                StockName = "dfs",
                StockSymbol = "dffds"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public void CreateBuyOrder_ProperDetails()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Parse("2006-11-25"),
                Price = 10,
                StockName = "dfs",
                StockSymbol = "MSFT"
            };

            BuyOrderResponse buy_order_response = _stocksService.CreateBuyOrder(request);

            Assert.True(buy_order_response.BuyOrderID != Guid.Empty);
        }

        #endregion

        #region CreateSellOrder

        [Fact]
        public void CreateSellOrder_SellOrderRequestIsNull()
        {
            SellOrderRequest? request = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_SellOrderQuantityIsZero()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 0,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 45,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_SellOrderQuantityIsOutOfRange()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100001,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 45,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_SellOrderPriceIsZero()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 0,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_SellOrderPriceIsOutOfRange()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 10001,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_StockSymbolISNull()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 10,
                StockName = "dfs",
                StockSymbol = null
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_DateAndTimeOfOrderIsOutOfRange()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31"),
                Price = 10,
                StockName = "dfs",
                StockSymbol = "dffds"
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public void CreateSellOrder_ProperDetails()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Parse("2006-11-25"),
                Price = 10,
                StockName = "dfs",
                StockSymbol = "MSFT"
            };

            SellOrderResponse sell_order_response = _stocksService.CreateSellOrder(request);

            Assert.True(sell_order_response.SellOrderID != Guid.Empty);
        }

        #endregion

        #region GetBuyOrders

        [Fact]
        public void GetBuyOrders_EmptyList()
        {
            BuyOrderRequest? request = new BuyOrderRequest();

            List<BuyOrderResponse> buyOrderResponses = _stocksService.GetBuyOrders();

            Assert.Empty(buyOrderResponses);
        }

        [Fact]
        public void GetBuyOrders_AddFewBuyOrders()
        {
            BuyOrderRequest? request1 = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 10,
                StockName = "dsf",
                StockSymbol = "dfs"
            };

            BuyOrderRequest? request2 = new BuyOrderRequest()
            {
                Quantity = 200,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 130,
                StockName = "dsf",
                StockSymbol = "dfs"
            };

            BuyOrderRequest? request3 = new BuyOrderRequest()
            {
                Quantity = 10,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 103,
                StockName = "sfs",
                StockSymbol = "sdf"
            };

            List<BuyOrderRequest> buy_order_responses_list = new List<BuyOrderRequest>() { request1, request2, request3 };

            List<BuyOrderResponse> buy_order_responses_from_add = new List<BuyOrderResponse>();

            foreach (var request in buy_order_responses_list)
            {
                buy_order_responses_from_add.Add(_stocksService.CreateBuyOrder(request));
            }

            List<BuyOrderResponse> actual_buy_order_response = _stocksService.GetBuyOrders();

            foreach (var expected_buy_order in buy_order_responses_from_add)
            {
                Assert.Contains(expected_buy_order, actual_buy_order_response);
            }
        }

        #endregion

        #region GetSellOrders

        [Fact]
        public void GetSellOrders_EmptyList()
        {
            SellOrderRequest? request = new SellOrderRequest();

            List<SellOrderResponse> sellOrderResponses = _stocksService.GetSellOrders();

            Assert.Empty(sellOrderResponses);
        }

        [Fact]
        public void GetSellOrders_AddFewSellOrders()
        {
            SellOrderRequest? request1 = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 10,
                StockName = "dsf",
                StockSymbol = "dfs"
            };

            SellOrderRequest? request2 = new SellOrderRequest()
            {
                Quantity = 200,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 130,
                StockName = "dsf",
                StockSymbol = "dfs"
            };

            SellOrderRequest? request3 = new SellOrderRequest()
            {
                Quantity = 10,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 103,
                StockName = "sfs",
                StockSymbol = "sdf"
            };

            List<SellOrderRequest> sell_order_responses_list = new List<SellOrderRequest>() { request1, request2, request3 };

            List<SellOrderResponse> sell_order_responses_from_add = new List<SellOrderResponse>();

            foreach (var request in sell_order_responses_list)
            {
                sell_order_responses_from_add.Add(_stocksService.CreateSellOrder(request));
            }

            List<SellOrderResponse> actual_sell_order_response = _stocksService.GetSellOrders();

            foreach (var expected_sell_order in sell_order_responses_from_add)
            {
                Assert.Contains(expected_sell_order, actual_sell_order_response);
            }
        }

        #endregion
    }
}
