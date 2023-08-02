using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.ContractResponses;

namespace LagoaTrading.Application.Service.Implementations
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IApplicationRepositories applicationRepositories;

        public UserAccountService(IApplicationRepositories applicationRepositories)
        {
            this.applicationRepositories = applicationRepositories;
        }

        public async Task<IEnumerable<UserAccountCurrency>> GetAccountsByHash(string hash)
        => await this.applicationRepositories.UserAccountRepository.GetAccountsByHash(hash);

        public Task<IEnumerable<User>> GetActiveUser(long userId)
        => this.applicationRepositories.UserRepository.GetActiveUser(userId);

        public Task<IEnumerable<UserAccount>> GetByUserId(long id)
        => this.applicationRepositories.UserAccountRepository.GetByUserId(id);

        public Task Save(UserAccount userAccount)
        => this.applicationRepositories.UserAccountRepository.Save(userAccount);
    }
}
