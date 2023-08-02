using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.ContractResponses;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly LagoaTradingContext context;

        public UserAccountRepository(LagoaTradingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<UserAccountCurrency>> GetAccountsByHash(string hash)
        => await this.context.UserAccount.Where(x => x.User.RollingHash == hash)
            .Select(u => new UserAccountCurrency
            {
                Symbol = u.Currency.Symbol,
                Amount = u.Balance,
            })
            .ToListAsync();

        public async Task<IEnumerable<UserAccount>> GetByUserId(long id)
        => await this.context.UserAccount.Where(x => x.UserId == id).ToListAsync();

        public async Task Save(UserAccount userAccount)
        {
            if (userAccount.Id == default)
            {
                await this.context.UserAccount.AddAsync(userAccount);
            }
            else
            {
                this.context.UserAccount.Update(userAccount);
            }
            await this.context.SaveChangesAsync();
        }
    }
}
