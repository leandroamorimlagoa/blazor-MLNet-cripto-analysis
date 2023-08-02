using LagoaTrading.Shared.Extensions;
using LagoaTrading.Shared.Interfaces;

namespace LagoaTrading.Shared.ContractsRequests
{
    public class CircuitRequest : IHashRequest
    {
        public string Hash { get; set; }
        public DateTime? FromDate { get; set; } = DateTime.Now.Date.AddDays(-30);
        public int Take { get; set; } = 7;

        public CircuitRequest()
        {

        }

        public CircuitRequest(string hash)
        {
            var key = CryptoHelper.CreateRandomKey();
            Hash = CryptoHelper.Encrypt(hash, key) + key;
        }
    }
}
