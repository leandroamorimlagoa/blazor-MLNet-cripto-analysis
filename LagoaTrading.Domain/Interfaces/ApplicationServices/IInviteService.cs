using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface IInviteService
    {
        Task<Invite?> Get(string inviteCode);
        void Update(Invite invite);
    }
}
