using LagoaTrading.Domain.Core.Basics;

namespace LagoaTrading.Domain.Entities
{
    public class Invite : BaseEntity
    {
        public long? FromUserId { get; set; }
        public long? ToUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UsedAt { get; set; }

        public virtual User? FromUser { get; set; }
    }
}
