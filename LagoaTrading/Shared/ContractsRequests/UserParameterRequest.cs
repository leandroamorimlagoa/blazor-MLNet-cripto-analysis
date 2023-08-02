using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Shared.ContractsRequests
{
    public class UserParameterRequest
    {
        private decimal referenceValue = 100;
        private decimal accountBalanceCurrency = 0;
        private decimal referenceAbsoluteValue = 300;
        private decimal minimumCryptoValue = 1;
        private decimal maximumCryptoValue = 1000;
        private decimal percentageToDecreaseToBuy = 0.5m;
        private decimal percentageToIncreaseToSell = 3;

        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;


        public ParameterTypeValue TypeValue { get; set; } = ParameterTypeValue.Percentage;
        public decimal ReferenceValue { get => referenceValue.TruncateCrypto(); set => referenceValue = value; }
        public decimal AccountBalanceCurrency { get => accountBalanceCurrency.TruncateCrypto(); set => accountBalanceCurrency = value; }
        public decimal ReferenceAbsoluteValue { get => referenceAbsoluteValue.TruncateCrypto(); set => referenceAbsoluteValue = value; }

        public bool OnlyPositiveCryptos { get; set; } = true;
        public decimal MinimumCryptoValue { get => minimumCryptoValue.TruncateCrypto(); set => minimumCryptoValue = value; }
        public decimal MaximumCryptoValue { get => maximumCryptoValue.TruncateCrypto(); set => maximumCryptoValue = value; }
        public decimal PercentageToDecreaseToBuy { get => percentageToDecreaseToBuy.TruncateCrypto(); set => percentageToDecreaseToBuy = value; }
        public decimal PercentageToIncreaseToSell { get => percentageToIncreaseToSell.TruncateCrypto(); set => percentageToIncreaseToSell = value; }
        public decimal AvaliableValue { get; set; }
        public CircuitCommand CircuitCommand { get; set; } = CircuitCommand.Stopped;

        public UserParameterRequest()
        {

        }

        public UserParameterRequest(string apiKey, string apiSecret)
        {
            var key = CryptoHelper.CreateRandomKey();
            ApiKey = CryptoHelper.Encrypt(apiKey, key) + key;
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
