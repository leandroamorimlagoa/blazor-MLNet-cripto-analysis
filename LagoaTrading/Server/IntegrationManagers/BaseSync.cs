using LagoaTrading.Domain.Interfaces.ApplicationServices;

namespace LagoaTrading.Server.IntegrationManagers
{
    public class BaseSync
    {
        private readonly string SyncName;
        protected IApplicationService applicationService;

        public BaseSync(string syncName, IApplicationService applicationService)
        {
            this.applicationService = applicationService;
            SyncName = syncName;
        }

        protected async Task RegisterSync(string name, DateTime? now = null)
        {
            if (now == null)
            {
                now = DateTime.UtcNow;
            }
            await this.applicationService.SyncControlService.Save(name, now.Value);
        }
    }
}
