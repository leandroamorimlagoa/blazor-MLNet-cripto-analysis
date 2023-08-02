namespace LagoaTrading.Shared.ContractResponses
{
    public class AnalysisResponse
    {
        public long MarketId { get; set; }
        public string Symbol { get; set; }
        public decimal SumVolume { get; set; }
        public decimal MaxPriceHighest { get; set; }
        public decimal MinPriceLowest { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal StartPriceOpen { get; set; }
        public decimal EndPriceClose { get; set; }
        public decimal ResultPrice { get; set; }
    }
}
