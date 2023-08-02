using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Shared.ApiResultObjects
{
    public class Analysis
    {
        private decimal sumVolume;
        private decimal maxPriceHighest;
        private decimal minPriceLowest;
        private decimal avgPrice;
        private decimal startPriceOpen;
        private decimal endPriceClose;
        private decimal resultPrice;

        public long MarketId { get; set; }
        public string Symbol { get; set; }
        public decimal SumVolume { get => sumVolume.TruncateCrypto(); set => sumVolume = value; }
        public decimal MaxPriceHighest { get => maxPriceHighest.TruncateCurrency(); set => maxPriceHighest = value; }
        public decimal MinPriceLowest { get => minPriceLowest.TruncateCurrency(); set => minPriceLowest = value; }
        public decimal AvgPrice { get => avgPrice.TruncateCurrency(); set => avgPrice = value; }
        public decimal StartPriceOpen { get => startPriceOpen.TruncateCurrency(); set => startPriceOpen = value; }
        public decimal EndPriceClose { get => endPriceClose.TruncateCurrency(); set => endPriceClose = value; }
        public decimal ResultPrice { get => resultPrice.TruncateCurrency(); set => resultPrice = value; }
    }
}
