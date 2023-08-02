using LagoaTrading.Data.Service.Extensions;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Shared.Core;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;

namespace LagoaTrading.Server.IntegrationManagers
{
    public class SyncOrder : BaseSync
    {
        public SyncOrder(IApplicationService applicationService)
                  : base(Constants.SyncControlNames.UserOrdersName, applicationService)
        {
        }

        internal async Task Execute(int userId = 0)
        {
            var positions = await this.applicationService.PositionService.GetActivePositionsFromUser(userId);
            if (positions == null || !positions.Any())
            {
                return;
            }

            foreach (var position in positions)
            {
                var user = await this.applicationService.UserService.GetUserById(position.UserId);
                if (user == null
                    || user.Parameter == null)
                {
                    continue;
                }

                var parameter = user.Parameter;
                var apiKey = CryptoHelper.Decrypt(parameter.ApiKey, position.User.EmailHash);
                var apiSecret = CryptoHelper.Decrypt(parameter.ApiSecret, position.User.EmailHash);

                var foxbitResponse = await this.applicationService.FoxbitService.RequestPosition(apiKey, apiSecret, position.OrderId);
                if (foxbitResponse == null)
                {
                    continue;
                }

                position.State = foxbitResponse.GetState();
                position.ResponsePrice = Convert.ToDecimal(foxbitResponse.Price);
                position.ResponsePriceAVG = Convert.ToDecimal(foxbitResponse.PriceAvg);
                position.Quantity = Convert.ToDecimal(foxbitResponse.Quantity);
                position.QuantityExecuted = Convert.ToDecimal(foxbitResponse.QuantityExecuted);
                // TODO buscar a data real de finalização da posição. Ver Trades Info
                position.Executed = DateTime.UtcNow;
                position.TradeCounts = foxbitResponse.TradesCount;

                await applicationService.PositionService.Save(position);

                if (!position.CircuitId.HasValue
                    || position.State != State.Filled)
                {
                    continue;
                }

                if (position.Side == Side.Buy)
                {
                    await applicationService.CircuitService.CreatePositionToSell(position.CircuitId.Value, position);
                }
                else
                {
                    await applicationService.CircuitService.EndCircuit(position.CircuitId.Value);
                }
            }
        }
    }
}