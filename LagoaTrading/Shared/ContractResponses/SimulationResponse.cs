namespace LagoaTrading.Shared.ContractResponses
{
    public class SimulationResponse
    {
        public long MarketId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }

        // TO BUY
        public decimal UnitPriceToBuy { get; set; }
        public decimal QuantityToBuy { get; set; }
        public decimal TaxCryptoToBuy { get; set; }
        public decimal TaxCurrencyToBuy { get; set; }
        public decimal TotalCryptoToBuy { get; set; }
        public decimal TotalCurrencyToBuy { get; set; }

        // TO SELL
        public decimal UnitPriceToSell { get; set; }
        public decimal QuantityToSell { get; set; }
        public decimal TaxCryptoToSell { get; set; }
        public decimal TaxCurrencyToSell { get; set; }
        public decimal TotalCurrencyToSell { get; set; }

        // RESULTS
        public decimal InicialValue { get; set; }
        public decimal FinalValue { get; set; }
        public decimal ElapsedTime { get; set; }
        public decimal Result { get; set; }
        public decimal TotalPriceToBuy { get; set; }
    }
}
