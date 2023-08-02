using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;
using LagoaTrading.Shared.Objects;

namespace LagoaTrading.Domain.Extensions
{
    public static class SimulationResponseExtensions
    {
        public static void LoadSimulationToBuy(this SimulationPosition position, Parameter parameter, ResponseQuote dataToBuy)
        {
            if (position == null)
            {
                position = new SimulationPosition();
            }
            position.Side = Side.Buy;
            position.UnitPrice = (dataToBuy.Price * (1 - (parameter.PercentageToDecreaseToBuy / 100))).TruncateCrypto();
            position.Quantity = dataToBuy.BaseAmount;

            position.UnitPrice = (dataToBuy.Price * (1 - (parameter.PercentageToDecreaseToBuy / 100))).TruncateCrypto();
            position.Quantity = dataToBuy.BaseAmount.TruncateCrypto();
            position.TaxCrypto = (position.Quantity * (parameter.PercentageToDecreaseToBuy / 100)).TruncateCrypto();
            position.TaxCurrency = (position.UnitPrice * position.TaxCrypto).TruncateCrypto();
            position.TotalCrypto = (position.Quantity - position.TaxCrypto).TruncateCrypto();
            position.TotalCurrency = (position.TotalCrypto * position.UnitPrice).TruncateCrypto();
        }

        public static void LoadSimulationToSell(this SimulationPosition position, Parameter parameter, decimal baseUnitPrice, decimal TotalCryptoToSell)
        {
            if (position == null)
            {
                position = new SimulationPosition();
            }
            position.UnitPrice = (baseUnitPrice * (1 + (parameter.PercentageToIncreaseToSell / 100))).TruncateCrypto();
            position.Quantity = TotalCryptoToSell;
            position.TaxCrypto = (TotalCryptoToSell * (parameter.PercentageToDecreaseToBuy / 100)).TruncateCrypto();
            position.TaxCurrency = (position.UnitPrice * position.TaxCrypto).TruncateCrypto();
            position.TotalCurrency = ((position.Quantity - position.TaxCrypto) * position.UnitPrice).TruncateCrypto();
        }
    }
}
