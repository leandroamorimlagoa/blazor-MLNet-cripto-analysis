using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.ContractResponses;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface IUserAccountRepository
    {
        Task<IEnumerable<UserAccountCurrency>> GetAccountsByHash(string hash);
        Task<IEnumerable<UserAccount>> GetByUserId(long id);
        Task Save(UserAccount userAccount);
    }
}
