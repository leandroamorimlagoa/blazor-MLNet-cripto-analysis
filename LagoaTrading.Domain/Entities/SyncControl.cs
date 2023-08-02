using LagoaTrading.Domain.Core.Basics;

namespace LagoaTrading.Domain.Entities
{
    public class SyncControl : BaseEntity
    {
        public string Name { get; set; }
        public DateTime LastSync { get; set; }
    }
}
