using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;


namespace Repositories
{
    public class StocksRepository(ApplicationDbContext stockMarketDbContext) : IStocksRepository
    {
        private readonly ApplicationDbContext _db = stockMarketDbContext;


        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            await _db.BuyOrders.AddAsync(buyOrder);
            await _db.SaveChangesAsync();

            return buyOrder;
        }

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            await _db.SellOrders.AddAsync(sellOrder);
            await _db.SaveChangesAsync();

            return sellOrder;
        }


        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _db.BuyOrders
             .AsNoTracking()
             .OrderByDescending(temp => temp.DateAndTimeOfOrder)
             .ToListAsync();

            return buyOrders;
        }


        public async Task<List<SellOrder>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _db.SellOrders
            .AsNoTracking()
            .OrderByDescending(temp => temp.DateAndTimeOfOrder)
            .ToListAsync();

            return sellOrders;
        }
    }
}

