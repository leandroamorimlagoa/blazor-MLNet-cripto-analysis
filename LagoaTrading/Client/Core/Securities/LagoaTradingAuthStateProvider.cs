using Blazored.LocalStorage;
using LagoaTrading.Shared.ContractResponses;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Text.Json;

namespace LagoaTrading.Client.Core.Securities
{
    public class LagoaTradingAuthStateProvider : AbstractLagoaTradingAuthStateProvider
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly NavigationManager navigationManager;

        public LagoaTradingAuthStateProvider(ISyncLocalStorageService localStorage
                                                , NavigationManager navigationManager)
        {
            this.localStorage = localStorage;
            this.navigationManager = navigationManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = localStorage.GetItem<AuthorizationResponse>(ConstantNames.LocalStoreNames.AuthenticationToken);
            //AuthorizationResponse token = null;
            //var token = new AuthorizationResponse
            //{
            //    ExpireIn = new DateTime(2023, 04, 23),
            //    Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJMZWFuZHJvIiwidW5pcXVlX25hbWUiOiJMZWFuZHJvIiwiZW1haWwiOiJsZWFuZHJvX2Ftb3JpbTZAaG90bWFpbC5jb20iLCJuYmYiOjE2Nzk4MzE1MDAsImV4cCI6MTY4MjM3NzIwMCwiaWF0IjoxNjc5ODMxNTAwfQ.xahp7KuWF-mSscKnMUnTkHeycdBOrk3I-cJGfhKFc7s",
            //    Name = "Leandro"
            //};
            // TODO: Redirecionar para Login com mensagem de informação
            if (token == null || token.ExpireIn < DateTime.Now)
            {
                localStorage.RemoveItem(ConstantNames.LocalStoreNames.AuthenticationToken);
                return new AuthenticationState(new ClaimsPrincipal());
            }
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token.Token), "jwt");

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public override async Task<bool> IsAuthenticated()
        {
            var authenticationState = await GetAuthenticationStateAsync();
            var isAuthenticated = authenticationState.User.Identity != null && authenticationState.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                await CleanLocalStorage();
            }
            return isAuthenticated;
        }


        public override async Task<string> GetUserHash()
        {
            var authenticationState = await GetAuthenticationStateAsync();
            if (authenticationState.User.Identity != null && !authenticationState.User.Identity.IsAuthenticated)
            {
                await CleanLocalStorage();
            }
            var hash = authenticationState.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Hash)?.Value;
            return hash;
        }
        private async Task CleanLocalStorage()
        {
            localStorage.RemoveItem(ConstantNames.LocalStoreNames.AuthenticationToken);
        }
    }
}
