using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Data.Service.Extensions
{
    public static class ResponseUserOrdersExtensions
    {
        public static State GetState(this ResponseUserOrders responseUserOrders)
        {
            switch (responseUserOrders.State)
            {
                case "ACTIVE":
                    return State.Active;
                case "FILLED":
                    return State.Filled;
                case "PARTIALLY_CANCELED":
                    return State.PartiallyCanceled;
                case "PARTIALLY_FILLED":
                    return State.PartiallyFilled;
                default:
                    return State.Cancelled;
            }
        }

        public static Side GetSide(this ResponseUserOrders responseUserOrders)
        {
            if (responseUserOrders.Side == "BUY")
            {
                return Side.Buy;
            }
            return Side.Sell;
        }

        public static OrderType GetItemType(this ResponseUserOrders responseUserOrders)
        {
            switch (responseUserOrders.Type)
            {
                case "MARKET":
                    return OrderType.Market;
                case "INSTANT":
                    return OrderType.Instant;
                case "STOP_MARKET":
                    return OrderType.StopMarket;
                default:
                    return OrderType.Limit;
            }
        }
    }
}
