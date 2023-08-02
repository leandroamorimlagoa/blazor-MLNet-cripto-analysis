using Microsoft.AspNetCore.Components.Authorization;

namespace LagoaTrading.Client.Core.Securities
{
    public abstract class AbstractLagoaTradingAuthStateProvider : AuthenticationStateProvider
    {
        public abstract Task<bool> IsAuthenticated();

        public abstract Task<string> GetUserHash();
    }
}
