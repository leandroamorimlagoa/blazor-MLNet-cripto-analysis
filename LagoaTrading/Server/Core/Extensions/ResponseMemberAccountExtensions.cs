using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;

namespace LagoaTrading.Server.Core.Extensions
{
    public static class ResponseMemberAccountExtensions
    {
        public static UserAccount ToUserAccount(this ResponseMemberAccount responseMemberAccount, long userId, long currencyId)
        {
            return new UserAccount
            {
                UserId = userId,
                CurrencyId = currencyId,
                Balance = responseMemberAccount.Balance,
                BalanceBlocked = responseMemberAccount.BalanceBlocked,
                BalanceAvailable = responseMemberAccount.BalanceAvailable
            };
        }
    }
}
