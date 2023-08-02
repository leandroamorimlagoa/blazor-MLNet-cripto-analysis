using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.Objects;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class CandlestickRepository : ICandlestickRepository
    {
        private readonly LagoaTradingContext context;

        public CandlestickRepository(LagoaTradingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CandlestickJsonObject>> GetAll()
        {
            var markets = this.context.Market.ToDictionary(m => m.Id, m => m.Symbol);
            var result = await this.context.Candlestick.Select(c => new CandlestickJsonObject
                                                                {
                                                                    DateTime = c.DateTime,
                                                                    MarketId = c.MarketId,
                                                                    MarketSymbol = markets[c.MarketId],
                                                                    Open = c.PriceOpen,
                                                                    Close = c.PriceClose,
                                                                    High = c.PriceHighest,
                                                                    Low = c.PriceLowest,
                                                                    Volume = c.Volume
                                                                })
                                                                .ToListAsync();
            
            return result;
        }

        public async Task Save(IEnumerable<Candlestick> candlesticks)
        {
            lock (this.context)
            {
                var candlesticksToClear = candlesticks
                                                    .OrderBy(c => c.DateTime)
                                                    .GroupBy(c => c.MarketId)
                                                    .ToDictionary(c => c.Key, c => c.Min(f => f.DateTime));

                ClearDuplicateCandlestics(candlesticksToClear);

                this.context.Candlestick.AddRange(candlesticks);
                this.context.SaveChanges();
            }
        }

        private void ClearDuplicateCandlestics(Dictionary<long, DateTime> candlesticksToClear)
        {
            foreach (var candlestick in candlesticksToClear)
            {
                var candlesticks = context.Candlestick.Where(c => c.MarketId == candlestick.Key && c.DateTime >= candlestick.Value);
                context.Candlestick.RemoveRange(candlesticks);
                context.SaveChanges();
            }
        }
    }
}
