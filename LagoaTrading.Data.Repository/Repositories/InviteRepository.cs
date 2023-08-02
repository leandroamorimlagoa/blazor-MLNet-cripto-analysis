using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class InviteRepository : IInviteRepository
    {
        private readonly LagoaTradingContext context;

        public InviteRepository(LagoaTradingContext context)
        {
            this.context = context;
        }

        public async Task<Invite?> Get(string inviteCode)
        => await context.Invite.FirstOrDefaultAsync(i => i.Code == inviteCode && !i.UsedAt.HasValue);

        public void Update(Invite invite)
        {
            this.context.Update(invite);
            this.context.SaveChanges();
        }
    }
}
