using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Shared.ContractsRequests
{
    public class UserPositionsRequest
    {
        public string? Hash { get; set; } 
        public State? State { get; set; }
        public int Take { get; set; } = 5;

        public UserPositionsRequest()
        {
            
        }
        
        public UserPositionsRequest(string hash)
        {
            var key = CryptoHelper.CreateRandomKey();
            Hash = CryptoHelper.Encrypt(hash, key) + key;
        }
    }
}
