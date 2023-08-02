using LagoaTrading.Domain.Core.Basics;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Domain.Entities
{
    public class Parameter : BaseEntity
    {
        public long UserId { get; set; }
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;


        public ParameterTypeValue TypeValue { get; set; } = ParameterTypeValue.Percentage;
        public decimal ReferenceValue { get; set; } = 100;
        public decimal AccountBalanceCurrency { get; set; } = 0;
        public decimal ReferenceAbsoluteValue { get; set; } = 0;
        public decimal AvaliableValue { get; set; } = 0;


        public bool OnlyPositiveCryptos { get; set; } = true;
        public decimal MinimumCryptoValue { get; set; } = 1;
        public decimal MaximumCryptoValue { get; set; } = 1000;



        public decimal PercentageToDecreaseToBuy { get; set; } = 0.5m;
        public decimal PercentageToIncreaseToSell { get; set; } = 3;
        public CircuitCommand CircuitCommand { get; set; } = CircuitCommand.Stopped;

        public User User { get; set; }
    }
}
