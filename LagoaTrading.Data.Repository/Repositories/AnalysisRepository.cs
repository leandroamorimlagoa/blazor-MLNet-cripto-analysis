using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.ApiResultObjects;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly LagoaTradingContext context;

        public AnalysisRepository(LagoaTradingContext context)
        {
            this.context = context;
        }

        public async Task<Analysis?> GetAnalysis(User user, Parameter parameter, int fromLastHours)
        => (await this.GetAnalysisList(user, parameter, 1, fromLastHours)).FirstOrDefault();

        public async Task<IEnumerable<Analysis>> GetAnalysisList(User user, Parameter parameter, int rows, int fromLastHours)
        {
            var dateTimeLimit = DateTime.Now.AddHours((-1) * fromLastHours);

            var markets = await this.context.Market.Include(m => m.CurrencyBase)
                .Select(c => new { MarketSymbol = c.Symbol, CurrencyBaseSymbol = c.CurrencyBase.Symbol })
                .ToDictionaryAsync(c => c.MarketSymbol, c => c.CurrencyBaseSymbol);

            var list24hCandlestickGroupBySymbol = this.context.Candlestick
                .Include(a => a.Market)
                .ThenInclude(a => a.CurrencyBase)
                .Where(c => c.DateTime >= dateTimeLimit)
                .GroupBy(c => c.Market)
                .Select(g => new Analysis
                {
                    MarketId = g.Key.Id,
                    Symbol = markets[g.Key.Symbol],
                    SumVolume = g.Sum(c => c.Volume),
                    MaxPriceHighest = g.Max(c => c.PriceHighest),
                    MinPriceLowest = g.Min(c => c.PriceLowest),
                    AvgPrice = g.Average(c => c.PriceOpen),
                    StartPriceOpen = g.First().PriceOpen,
                    EndPriceClose = g.OrderBy(c => c.DateTime).Last().PriceClose,
                    ResultPrice = g.OrderBy(c => c.DateTime).Last().PriceClose - g.OrderBy(c => c.DateTime).First().PriceOpen
                });

            if (parameter.OnlyPositiveCryptos)
            {
                list24hCandlestickGroupBySymbol = list24hCandlestickGroupBySymbol.Where(c => c.ResultPrice > 0);
            }

            list24hCandlestickGroupBySymbol = list24hCandlestickGroupBySymbol.Where(c => c.AvgPrice >= parameter.MinimumCryptoValue && c.AvgPrice <= parameter.MaximumCryptoValue);

            list24hCandlestickGroupBySymbol = list24hCandlestickGroupBySymbol.OrderByDescending(g => g.SumVolume);

            return await list24hCandlestickGroupBySymbol.Take(rows).ToListAsync();
        }
    }
}
