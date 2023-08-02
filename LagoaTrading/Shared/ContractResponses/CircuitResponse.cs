namespace LagoaTrading.Shared.ContractResponses
{
    public class CircuitResponse
    {
        public List<UserPositionsResponse> PositionBuy { get; set; }
        public List<UserPositionsResponse> PositionSell { get; set; }
        public decimal Profit { get; set; }
        public decimal DurationHours { get; set; }
    }
}
