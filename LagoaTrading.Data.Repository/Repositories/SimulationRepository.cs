using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Extensions;
using LagoaTrading.Domain.Interfaces.FoxbitServices;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Domain.Objects.FoxbitObjects.Requests;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Objects;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class SimulationRepository : ISimulationRepository
    {
        private readonly IFoxbitHttpClient foxbitClient;
        private readonly LagoaTradingContext context;

        public SimulationRepository(IFoxbitHttpClient foxbitClient,
                                     LagoaTradingContext context)
        {
            this.foxbitClient = foxbitClient;
            this.context = context;
        }

        public async Task<SimulationPosition> SimulateBuy(Parameter parameter, long marketId)
        {
            var market = await context.Market.Include(m => m.CurrencyBase)
                                             .Include(m => m.CurrencyQuote)
                                             .FirstOrDefaultAsync(m => m.Id == marketId);
            if (market == null)
            {
                return null;
            }

            var requestQuoteToBuy = new RequestQuote(Side.Buy, market.CurrencyBase.Symbol, market.CurrencyQuote.Symbol, parameter.AvaliableValue);
            var dataToBuy = await foxbitClient.Get<ResponseQuote>(requestQuoteToBuy);
            if (dataToBuy.BaseAmount < 1)
            {
                return null;
            }
            var result = new SimulationPosition();
            result.LoadSimulationToBuy(parameter, dataToBuy);
            return result;
        }

        public SimulationPosition SimulateSell(Parameter parameter, decimal baseUnitPrice, decimal TotalCryptoToSell)
        {
            var result = new SimulationPosition();
            result.LoadSimulationToSell(parameter, baseUnitPrice, TotalCryptoToSell);
            return result;
        }
    }
}
