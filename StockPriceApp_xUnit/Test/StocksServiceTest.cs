using Services;
using ServiceContracts;
using ServiceContracts.DTO;
using Microsoft.EntityFrameworkCore;
using Entities;
using System.Threading.Tasks;

namespace Test
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;

        public StocksServiceTest()
        {
            _stocksService = new StocksService(new StockMarketDbContext(new DbContextOptionsBuilder<StockMarketDbContext>().Options));
        }

        #region CreateBuyOrder

        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestIsNull()
        {
            BuyOrderRequest? request = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_BuyOrderQuantityIsZero()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 0,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 45,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_BuyOrderQuantityIsOutOfRange()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100001,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 45,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
               await _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_BuyOrderPriceIsZero()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 0,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

           await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
              await  _stocksService.CreateBuyOrder(request);
            });
        }


        [Fact]
        public async Task CreateBuyOrder_BuyOrderPriceIsOutOfRange()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 10001,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
               await _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_StockSymbolISNull()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 10,
                StockName = "dfs",
                StockSymbol = null
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
               await _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_DateAndTimeOfOrderIsOutOfRange()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31"),
                Price = 10,
                StockName = "dfs",
                StockSymbol = "dffds"
            };

           await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
              await  _stocksService.CreateBuyOrder(request);
            });
        }

        [Fact]
        public async Task CreateBuyOrder_ProperDetails()
        {
            BuyOrderRequest? request = new BuyOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Parse("2006-11-25"),
                Price = 10,
                StockName = "dfs",
                StockSymbol = "MSFT"
            };

            BuyOrderResponse buy_order_response = await _stocksService.CreateBuyOrder(request);

            Assert.True(buy_order_response.BuyOrderID != Guid.Empty);
        }

        #endregion

        #region CreateSellOrder

        [Fact]
        public async Task CreateSellOrder_SellOrderRequestIsNull()
        {
            SellOrderRequest? request = null;

           await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
              await  _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public async Task CreateSellOrder_SellOrderQuantityIsZero()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 0,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 45,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
               await _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public async Task CreateSellOrder_SellOrderQuantityIsOutOfRange()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100001,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 45,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
              await  _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public async Task CreateSellOrder_SellOrderPriceIsZero()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 0,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

           await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
              await  _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public async Task CreateSellOrder_SellOrderPriceIsOutOfRange()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 10001,
                StockName = "dfs",
                StockSymbol = "fdsf"
            };

           await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
               await _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public async Task CreateSellOrder_StockSymbolISNull()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Price = 10,
                StockName = "dfs",
                StockSymbol = null
            };

           await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
               await _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public async Task CreateSellOrder_DateAndTimeOfOrderIsOutOfRange()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31"),
                Price = 10,
                StockName = "dfs",
                StockSymbol = "dffds"
            };

           await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
             await   _stocksService.CreateSellOrder(request);
            });
        }

        [Fact]
        public async Task CreateSellOrder_ProperDetails()
        {
            SellOrderRequest? request = new SellOrderRequest()
            {
                Quantity = 100,
                DateAndTimeOfOrder = DateTime.Parse("2006-11-25"),
                Price = 10,
                StockName = "dfs",
                StockSymbol = "MSFT"
            };

            SellOrderResponse sell_order_response = await _stocksService.CreateSellOrder(request);

            Assert.True(sell_order_response.SellOrderID != Guid.Empty);
        }

        #endregion

        #region GetBuyOrders

        [Fact]
        public async Task GetBuyOrders_EmptyList()
        {
            BuyOrderRequest? request = new BuyOrderRequest();

            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();

            Assert.Empty(buyOrderResponses);
        }

        [Fact]
        public async Task GetBuyOrders_AddFewBuyOrders()
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
                buy_order_responses_from_add.Add(await _stocksService.CreateBuyOrder(request));
            }

            List<BuyOrderResponse> actual_buy_order_response = await _stocksService.GetBuyOrders();

            foreach (var expected_buy_order in buy_order_responses_from_add)
            {
                Assert.Contains(expected_buy_order, actual_buy_order_response);
            }
        }

        #endregion

        #region GetSellOrders

        [Fact]
        public async Task GetSellOrders_EmptyList()
        {
            SellOrderRequest? request = new SellOrderRequest();

            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

            Assert.Empty(sellOrderResponses);
        }

        [Fact]
        public async Task GetSellOrders_AddFewSellOrders()
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
                sell_order_responses_from_add.Add(await _stocksService.CreateSellOrder(request));
            }

            List<SellOrderResponse> actual_sell_order_response = await _stocksService.GetSellOrders();

            foreach (var expected_sell_order in sell_order_responses_from_add)
            {
                Assert.Contains(expected_sell_order, actual_sell_order_response);
            }
        }

        #endregion
    }
}
