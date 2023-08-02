using LagoaTrading.Domain.Core.Basics;

namespace LagoaTrading.Domain.Entities
{
    public class Market : BaseEntity
    {
        public string Symbol { get; set; }
        public decimal QuantityMin { get; set; }
        public decimal QuantityIncrement { get; set; }
        public decimal PriceMin { get; set; }
        public decimal PriceIncrement { get; set; }
        public long CurrencyBaseId { get; set; }
        public long CurrencyQuoteId { get; set; }

        public virtual Currency CurrencyBase { get; set; }
        public virtual Currency CurrencyQuote { get; set; }

        //public virtual ICollection<Candlestick> Candlestick { get; set; }
        //public virtual ICollection<Quotation> Quotation { get; set; }
    }
}
