using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Shared.Core;

namespace LagoaTrading.Server.IntegrationManagers
{
    public class SyncMarket : BaseSync
    {
        public SyncMarket(IApplicationService applicationService)
                   : base(Constants.SyncControlNames.MarketsName, applicationService)
        {
        }

        internal async Task Execute()
        {
            var foxbitMarkets = await this.applicationService.FoxbitService.GetMarkets();
            var currencies = (await this.applicationService.CurrencyService.GetAll()).ToDictionary(c => c.Symbol, c => c.Id);

            foreach (var item in foxbitMarkets)
            {
                if (!currencies.ContainsKey(item.Base.Symbol)
                    || !currencies.ContainsKey(item.Quote.Symbol))
                {
                    continue;
                }

                var market = await this.applicationService.MarketService.GetBySymbol(item.Symbol);
                if (market == null)
                {
                    market = new Market();
                }

                market.Symbol = item.Symbol;
                market.QuantityMin = item.QuantityMin;
                market.QuantityIncrement = item.QuantityIncrement;
                market.CurrencyBaseId = currencies[item.Base.Symbol];
                market.CurrencyQuoteId = currencies[item.Quote.Symbol];
                market.PriceMin = item.PriceMin;
                market.PriceIncrement = item.PriceIncrement;

                await this.applicationService.MarketService.Save(market);
            }
        }
    }
}
