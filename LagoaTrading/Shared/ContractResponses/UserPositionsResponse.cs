using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Shared.ContractResponses
{
    public class UserPositionsResponse
    {
        public Side Side { get; set; }
        public string Symbol { get; set; }
        public decimal QuantityExecuted { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public State State { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
