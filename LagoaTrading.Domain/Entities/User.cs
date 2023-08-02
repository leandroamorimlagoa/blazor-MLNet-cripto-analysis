using LagoaTrading.Domain.Core.Basics;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identifier { get; set; }
        public string EmailHash { get; set; }
        public string Password { get; set; }
        public string RollingHash { get; set; }
        public UserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }


        public virtual Parameter Parameter { get; set; }
        public virtual ICollection<UserAccount> UserAccount { get; set; }
        public virtual ICollection<Invite>? InviteUsers { get; set; }
        public virtual ICollection<Position>? Position { get; set; }

        public void NewRollingHash()
        {
            RollingHash = CryptoHelper.CreateRandomKey();
        }
    }
}
