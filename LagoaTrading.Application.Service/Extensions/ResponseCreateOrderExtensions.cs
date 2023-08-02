using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Objects.FoxbitObjects.Requests;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Application.Service.Extensions
{
    public static class ResponseCreateOrderExtensions
    {
        public static Position ToPosition(this RequestCreateOrder request, ResponseCreateOrder response, long userId, long marketId, Side side = Side.Buy)
        {
            var position = new Position
            {
                UserId = userId,
                MarketId = marketId,
                Side = side,
                OrderType = request.GetOrderType(),
                OrderId = response.Id,
                ClientOrderId = response.ClientOrderId,
                Quantity = Convert.ToDecimal(request.Quantity),
                UnitPrice = Convert.ToDecimal(request.UnitPrice),
                State = State.Registered,
                CreatedAt = DateTime.UtcNow,
                Created = DateTime.UtcNow,
            };

            return position;
        }
    }
}
