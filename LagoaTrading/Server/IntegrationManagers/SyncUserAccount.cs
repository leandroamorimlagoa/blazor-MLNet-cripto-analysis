using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Extensions;
using LagoaTrading.Shared.Core;
using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Server.IntegrationManagers
{
    public class SyncUserAccount : BaseSync
    {
        public SyncUserAccount(IApplicationService applicationService)
                        : base(Constants.SyncControlNames.UserAccountsName, applicationService)
        {
        }

        internal async Task Execute(long userId = 0)
        {
            var currencies = (await this.applicationService.CurrencyService.GetAll())
                                                    .ToDictionary(c => c.Symbol, c => c.Id);
            var activeUsers = await this.applicationService.UserAccountService.GetActiveUser(userId);
            foreach (var user in activeUsers)
            {
                var apiKey = CryptoHelper.Decrypt(user.Parameter.ApiKey, user.EmailHash);
                var apiSecret = CryptoHelper.Decrypt(user.Parameter.ApiSecret, user.EmailHash);

                var foxbitUserAccounts = await this.applicationService.FoxbitService.RequestMemberAccount(apiKey, apiSecret);
                var localUserAccounts = await this.applicationService.UserAccountService.GetByUserId(user.Id);
                if (localUserAccounts == null)
                {
                    localUserAccounts = new List<UserAccount>();
                }

                foreach (var item in foxbitUserAccounts)
                {
                    if (!currencies.ContainsKey(item.CurrencySymbol))
                    {
                        continue;
                    }

                    long currencyId = currencies[item.CurrencySymbol];
                    var userAccount = item.ToUserAccount(user.Id, currencyId);
                    var localUserAccount = localUserAccounts.FirstOrDefault(l => l.CurrencyId == currencyId);
                    if(localUserAccount == null)
                    {
                        localUserAccount = new UserAccount();
                        localUserAccount.UserId = user.Id;
                        localUserAccount.CurrencyId = currencyId;
                    }
                    localUserAccount.Balance = userAccount.Balance;
                    localUserAccount.BalanceAvailable = userAccount.BalanceAvailable;
                    localUserAccount.BalanceBlocked= userAccount.BalanceBlocked;

                    await this.applicationService.UserAccountService.Save(localUserAccount);
                }
            }
        }
    }
}
