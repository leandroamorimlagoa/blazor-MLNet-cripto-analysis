using LagoaTrading.Domain.Core.Basics;

namespace LagoaTrading.Domain.Entities
{
    public class UserAccount : BaseEntity
    {
        public long UserId { get; set; }
        public long CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public decimal BalanceAvailable { get; set; }
        public decimal BalanceBlocked { get; set; }

        public virtual User User { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
