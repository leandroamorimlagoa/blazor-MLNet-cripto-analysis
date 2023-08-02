using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.ContractResponses;

namespace LagoaTrading.Domain.Extensions
{
    public static class PositionToUserPositionExtensions
    {
        public static UserPositionsResponse ToUserPosition(this Position p)
        {
            return new UserPositionsResponse
            {
                Symbol = p.Market.CurrencyBase.Name,
                Side = p.Side,
                QuantityExecuted = p.QuantityExecuted,
                Total = p.QuantityExecuted * p.UnitPrice,
                Price = p.UnitPrice,
                State = p.State,
                CreatedAt = p.CreatedAt
            };
        }

        public static List<UserPositionsResponse> ToUserPosition(this ICollection<Position> positions)
        {
            return positions.Select(x => x.ToUserPosition()).ToList();
        }
    }
}
