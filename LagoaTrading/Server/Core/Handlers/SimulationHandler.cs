//using LagoaTrading.Data.Service.Requests;
//using LagoaTrading.Domain.Entities;
//using LagoaTrading.Domain.Interfaces.FoxbitServices;
//using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;
//using LagoaTrading.Shared.ContractResponses;
//using LagoaTrading.Shared.Enumerators;
//using LagoaTrading.Shared.Extensions;
//using LagoaTrading.Shared.Objects;

//namespace LagoaTrading.Server.Core.Handlers
//{
//    public class SimulationHandler
//    {
//        private readonly User user;
//        private readonly Parameter parameter;
//        private readonly Market market;
//        private readonly IFoxbitHttpClient foxbitClient;
//        public SimulationHandler(User user
//                                    , Parameter parameter
//                                    , Market market
//                                    , IFoxbitHttpClient foxbitClient)
//        {
//            this.user = user;
//            this.parameter = parameter;
//            this.market = market;
//            this.foxbitClient = foxbitClient;
//        }

//        public async Task<SimulationResponse> Handle()
//        {
//            if (market.CurrencyBase == null || market.CurrencyQuote == null)
//            {
//                return null;
//            }

//            var requestQuoteToBuy = new RequestQuote(Side.Buy, market.CurrencyBase.Symbol, market.CurrencyQuote.Symbol, parameter.AvaliableValue);
//            var dataToBuy = await foxbitClient.Get<ResponseQuote>(requestQuoteToBuy);
//            if (dataToBuy.BaseAmount < 1)
//            {
//                return null;
//            }


//            var result = new SimulationResponse();
//            var buy = HandleBuyPosition(dataToBuy.Price, dataToBuy.QuoteAmount);
//            var sell = HandleSellPosition(buy.UnitPrice, buy.TotalCrypto);

//            result.UnitPriceToBuy = buy.UnitPrice;
//            result.QuantityToBuy = buy.Quantity;
//            result.TaxCryptoToBuy = buy.TaxCrypto;
//            result.TaxCurrencyToBuy = buy.TaxCurrency;
//            result.TotalCryptoToBuy = buy.TotalCrypto;
//            result.TotalPriceToBuy = buy.TotalCrypto * buy.UnitPrice;

//            result.UnitPriceToSell = sell.UnitPrice;
//            result.QuantityToSell = sell.Quantity;
//            result.TaxCryptoToSell = sell.TaxCrypto;
//            result.TaxCurrencyToSell = sell.TaxCurrency;
//            result.TotalCurrencyToSell = sell.TotalCurrency;

//            result.InicialValue = dataToBuy.QuoteAmount.TruncateCurrency();
//            result.FinalValue = sell.TotalCurrency;
//            result.ElapsedTime = 0;

//            result.Result = (result.FinalValue - result.InicialValue).TruncateCurrency();
//            result.UnitPrice = dataToBuy.Price.TruncateCurrency();
//            result.MarketId = market.Id;
//            result.Symbol = market.Symbol;

//            return result;
//        }

//        public SimulationPosition HandleBuyPosition(decimal basePrice, decimal baseQuantity)
//        {
//            var position = new SimulationPosition();
//            position.UnitPrice = (basePrice * (1 - (parameter.PercentageToDecreaseToBuy / 100))).TruncateCrypto();
//            position.Quantity = baseQuantity.TruncateCrypto();
//            position.TaxCrypto = (position.Quantity * (parameter.PercentageToDecreaseToBuy / 100)).TruncateCrypto();
//            position.TaxCurrency = (position.UnitPrice * position.TaxCrypto).TruncateCrypto();
//            position.TotalCrypto = (position.Quantity - position.TaxCrypto).TruncateCrypto();
//            return position;
//        }

//        public SimulationPosition HandleSellPosition(decimal baseUnitPrice, decimal TotalCrypto)
//        {
//            var position = new SimulationPosition();
//            position.UnitPrice = (baseUnitPrice * (1 + (parameter.PercentageToIncreaseToSell / 100))).TruncateCrypto();
//            position.Quantity = TotalCrypto;
//            position.TaxCrypto = (TotalCrypto * (parameter.PercentageToDecreaseToBuy / 100)).TruncateCrypto();
//            position.TaxCurrency = (position.UnitPrice * position.TaxCrypto).TruncateCrypto();
//            position.TotalCurrency = ((position.Quantity - position.TaxCrypto) * position.UnitPrice).TruncateCrypto();

//            return position;
//        }
//    }
//}
