using LagoaTrading.Domain.Core.Basics;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Domain.Entities
{
    public class Position : BaseEntity
    {
        public long UserId { get; set; }
        public long MarketId { get; set; }
        public Side Side { get; set; }
        public OrderType OrderType { get; set; }
        public State State { get; set; }
        public string ClientOrderId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal QuantityExecuted { get; set; } = 0;
        public decimal ResponsePrice { get; set; } =0;
        public decimal ResponsePriceAVG { get; set; } = 0;
        public int TradeCounts { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Executed { get; set; }


        public long? CircuitId { get; set; }
        public long OrderId { get; set; }
        public string? Identifier { get; set; }

        public virtual Market Market { get; set; }
        public virtual User User { get; set; }
        public virtual Circuit Circuit { get; set; }
    }
}
