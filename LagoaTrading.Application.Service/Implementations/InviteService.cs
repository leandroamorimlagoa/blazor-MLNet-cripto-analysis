using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.Repositories;

namespace LagoaTrading.Application.Service.Implementations
{
    public class InviteService : IInviteService
    {
        private readonly IApplicationRepositories applicationRepositories;

        public InviteService(IApplicationRepositories applicationRepositories)
        {
            this.applicationRepositories = applicationRepositories;
        }

        public Task<Invite?> Get(string inviteCode)
        => this.applicationRepositories.InviteRepository.Get(inviteCode);

        public void Update(Invite invite)
        => this.applicationRepositories.InviteRepository.Update(invite);
    }
}
