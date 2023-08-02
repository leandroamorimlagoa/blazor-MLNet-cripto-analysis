using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.FoxbitServices;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Application.Service.Implementations
{
    public class SimulationService : ISimulationService
    {
        private readonly IFoxbitRepository foxbitRepository;
        private readonly IApplicationRepositories applicationRepositories;

        public SimulationService(IFoxbitRepository foxbitRepository,
                                  IApplicationRepositories applicationRepositories)
        {
            this.foxbitRepository = foxbitRepository;
            this.applicationRepositories = applicationRepositories;
        }

        public async Task<SimulationResponse> GetSimulation(Parameter parameter, Market market)
        {
            if (market.CurrencyBase == null || market.CurrencyQuote == null || parameter == null)
            {
                return null;
            }

            var dataToBuy = await foxbitRepository.RequestQuote(Side.Buy, market.CurrencyBase.Symbol, market.CurrencyQuote.Symbol, parameter.AvaliableValue);
            if (dataToBuy.BaseAmount < 1)
            {
                return null;
            }

            var result = new SimulationResponse();
            var buy = await this.applicationRepositories.SimulationRepository.SimulateBuy(parameter, market.Id);
            var sell = this.applicationRepositories.SimulationRepository.SimulateSell(parameter, buy.UnitPrice, buy.TotalCrypto);

            result.UnitPriceToBuy = buy.UnitPrice;
            result.QuantityToBuy = buy.Quantity;
            result.TaxCryptoToBuy = buy.TaxCrypto;
            result.TaxCurrencyToBuy = buy.TaxCurrency;
            result.TotalCryptoToBuy = buy.TotalCrypto;
            result.TotalPriceToBuy = buy.TotalCrypto * buy.UnitPrice;

            result.UnitPriceToSell = sell.UnitPrice;
            result.QuantityToSell = sell.Quantity;
            result.TaxCryptoToSell = sell.TaxCrypto;
            result.TaxCurrencyToSell = sell.TaxCurrency;
            result.TotalCurrencyToSell = sell.TotalCurrency;

            result.InicialValue = dataToBuy.QuoteAmount.TruncateCurrency();
            result.FinalValue = sell.TotalCurrency;
            result.ElapsedTime = 0;

            result.Result = (result.FinalValue - result.InicialValue).TruncateCurrency();
            result.UnitPrice = dataToBuy.Price.TruncateCurrency();
            result.MarketId = market.Id;
            result.Symbol = market.Symbol;

            return result;
        }
    }
}
