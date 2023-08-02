using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Shared.ContractResponses
{
    public class UserParameterResponse
    {
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;
        public bool OnlyPositiveCryptos { get; set; } = true;


        public ParameterTypeValue TypeValue { get; set; } = ParameterTypeValue.Percentage;
        public decimal ReferenceValue { get; set; } = 100;
        public decimal AccountBalanceCurrency { get; set; } = 0;
        public decimal ReferenceAbsoluteValue { get; set; } = 300;


        public decimal MinimumCryptoValue { get; set; } = 1;
        public decimal MaximumCryptoValue { get; set; } = 1000;

        public decimal PercentageToDecreaseToBuy { get; set; } = 0.5m;
        public decimal PercentageToIncreaseToSell { get; set; } = 3;
        public decimal AvaliableValue { get; set; }

        public UserParameterResponse()
        {

        }

        public UserParameterResponse(string apiKey, string apiSecret)
        {
            var key = CryptoHelper.CreateRandomKey();
            ApiKey = CryptoHelper.Encrypt(apiKey, key)+ key;
            ApiSecret = CryptoHelper.Encrypt(apiSecret, key);
        }

        public string GetApiKey()
        {
            var key = ApiKey.Substring(ApiKey.Length - 32);
            var cyphered = ApiKey.Substring(0, ApiKey.Length - 32);
            return CryptoHelper.Decrypt(cyphered, key);
        }

        public string GetApiSecret()
        {
            var key = ApiKey.Substring(ApiKey.Length - 32);
            return CryptoHelper.Decrypt(ApiSecret, key);
        }
    }
}
