using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface IInviteRepository
    {
        Task<Invite?> Get(string inviteCode);
        void Update(Invite invite);
    }
}
