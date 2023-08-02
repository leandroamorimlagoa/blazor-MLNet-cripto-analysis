using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.ContractResponses;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface IUserAccountService
    {
        Task<IEnumerable<UserAccountCurrency>> GetAccountsByHash(string hash);
        Task<IEnumerable<User>> GetActiveUser(long userId);
        Task<IEnumerable<UserAccount>> GetByUserId(long id);
        Task Save(UserAccount userAccount);
    }
}
