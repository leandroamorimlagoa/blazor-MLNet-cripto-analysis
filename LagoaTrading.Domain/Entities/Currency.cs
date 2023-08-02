using LagoaTrading.Domain.Core.Basics;

namespace LagoaTrading.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Precision { get; set; }

        public virtual ICollection<Market> MarketBase { get; set; }
        public virtual ICollection<Market> MarketQuote { get; set; }
        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
