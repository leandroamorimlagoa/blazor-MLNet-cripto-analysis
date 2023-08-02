using LagoaTrading.Domain.Interfaces.ApplicationServices;

namespace LagoaTrading.Server.IntegrationManagers
{
    public class NewFoxbitSyncManager
    {
        private readonly IApplicationService applicationService;

        public NewFoxbitSyncManager(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        internal void SyncUserAccount()
        {
            var sync = new SyncUserAccount(applicationService);
            sync.Execute().Wait();
        }

        internal void SyncCandlestick()
        {
            var sync = new SyncCandlestick(applicationService);
            sync.Execute().Wait();
        }

        internal void SyncCurrencies()
        {
            var sync = new SyncCurrency(applicationService);
            sync.Execute().Wait();
        }

        internal void SyncMarkets()
        {
            var sync = new SyncMarket(applicationService);
            sync.Execute().Wait();
        }

        internal void SyncOrders()
        {
            var sync = new SyncOrder(applicationService);
            sync.Execute().Wait();
        }

        internal void StartNewCircuits()
        {
            var sync = new SyncCircuit(applicationService);
            sync.Execute().Wait();
        }
    }
}
