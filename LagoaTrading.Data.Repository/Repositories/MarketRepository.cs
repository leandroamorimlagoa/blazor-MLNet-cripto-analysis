using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class MarketRepository : IMarketRepository
    {
        private readonly LagoaTradingContext context;

        public MarketRepository(LagoaTradingContext context)
        {
            this.context = context;
        }

        public async Task<Market?> Get(long marketId)
        => await context.Market.Include(m => m.CurrencyBase)
                               .Include(m => m.CurrencyQuote)
                               .FirstOrDefaultAsync(m => m.Id == marketId);

        public async Task<IEnumerable<Market>> GetAll()
        => await context.Market.Include(m => m.CurrencyBase)
                                 .Include(m => m.CurrencyQuote)
                                 .ToListAsync();

        public Task<Market?> GetBySymbol(string symbol)
        => context.Market.Include(m => m.CurrencyBase)
                         .Include(m => m.CurrencyQuote)
                         .FirstOrDefaultAsync(m => m.Symbol == symbol);

        public Task Save(Market market)
        {
            if (market.Id == 0)
            {
                context.Market.Add(market);
            }
            else
            {
                context.Market.Update(market);
            }
            return context.SaveChangesAsync();
        }
    }
}
