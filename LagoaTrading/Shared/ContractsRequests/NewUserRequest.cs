using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Shared.ContractsRequests
{
    public class NewUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ApiSecret { get; set; }
        public string ApiKey { get; set; }
        public string InviteCode { get; set; }

        public NewUserRequest(string userName, string password, string apiSecret, string apiKey, string inviteCode)
        {
            var key = CryptoHelper.CreateRandomKey();
            UserName = userName;
            Password = CryptoHelper.Encrypt(password, key) + key;
            ApiSecret = CryptoHelper.Encrypt(apiSecret, key);
            ApiKey = CryptoHelper.Encrypt(apiKey, key);
            InviteCode = inviteCode;
        }

        public NewUserRequest()
        {

        }
    }
}
