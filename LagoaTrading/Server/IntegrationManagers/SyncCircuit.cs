using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Shared.Core;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Server.IntegrationManagers
{
    public class SyncCircuit : BaseSync
    {
        public SyncCircuit(IApplicationService applicationService) : base(Constants.SyncControlNames.Circuit, applicationService)
        {
        }

        public async Task Execute()
        {
            var users = await this.applicationService.CircuitService.GetUsersToRunCircuit();
            if(users.Any())
            {
                var sync = new SyncCandlestick(this.applicationService);
                await sync.Execute();
            }

            foreach (var user in users)
            {
                var parameter = user.Parameter;
                await this.applicationService.CircuitService.StartCircuit(user, parameter, CircuitType.Continuous);
            }
        }
    }
}
