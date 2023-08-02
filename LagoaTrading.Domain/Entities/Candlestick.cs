using LagoaTrading.Domain.Core.Basics;

namespace LagoaTrading.Domain.Entities
{
    public class Candlestick : BaseEntity
    {
        public long MarketId { get; set; }
        public long DateTimeUTC { get; set; }
        public decimal PriceOpen { get; set; }
        public decimal PriceHighest { get; set; }
        public decimal PriceLowest { get; set; }
        public decimal PriceClose { get; set; }
        public decimal Volume { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Market Market { get; set; }
    }
}
