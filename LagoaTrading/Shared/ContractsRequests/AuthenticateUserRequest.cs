using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Shared.ContractsRequests
{
    public class AuthenticateUserRequest
    {
        public string Hash { get; set; }
        public string Password { get; set; }

        public AuthenticateUserRequest(string hash, string password)
        {
            var key = CryptoHelper.CreateRandomKey();
            Hash = CryptoHelper.Encrypt(hash, key) + key;
            Password = CryptoHelper.Encrypt(password, key);
        }

        public AuthenticateUserRequest()
        {
            Hash = "";
            Password = "";
        }
    }
}
