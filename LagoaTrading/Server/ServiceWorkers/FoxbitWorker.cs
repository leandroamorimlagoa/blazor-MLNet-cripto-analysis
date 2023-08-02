using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.IntegrationManagers;

namespace LagoaTrading.Server.ServiceWorkers
{
    public class FoxbitWorker : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly LagoaTradingConfiguration configuration;
        private NewFoxbitSyncManager syncManager;

        // timers
        private bool isBasicsSynced = false;
        private bool isRunningCandlestick = false;
        private Timer onceADayFire;
        private Timer recurrentFire;

        public FoxbitWorker(IServiceScopeFactory scopeFactory,
                            LagoaTradingConfiguration configuration)
        {
            this.scopeFactory = scopeFactory;
            this.configuration = configuration;

            this.SetupTimers();
        }

        private void SetupTimers()
        {
            recurrentFire = new Timer(SyncOperations, null, TimeSpan.Zero, TimeSpan.FromMinutes(this.configuration.Sync.OperationsIntervalMinutes));

            var executeTime = DateTime.Today.AddHours(this.configuration.Sync.BasicsOnceADayAt);
            TimeSpan timeUntilExecute = executeTime - DateTime.Now;
            if (timeUntilExecute < TimeSpan.Zero)
                timeUntilExecute = timeUntilExecute.Add(TimeSpan.FromDays(1));

            onceADayFire = new Timer(SyncOnceADay, null, TimeSpan.Zero, TimeSpan.FromMicroseconds(timeUntilExecute.TotalMicroseconds));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Task.Run(() => this.InitialExecution());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void InitialExecution()
        {
            this.SyncOnceADay(null);
            this.SyncOperations(null);
        }

        private void SyncOnceADay(object? state)
        {
            this.SyncCurrency();
            this.SyncMarket();
            this.isBasicsSynced = true;
        }

        private void SyncMarket()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<IApplicationService>();
                this.syncManager = new NewFoxbitSyncManager(services);
                this.syncManager.SyncMarkets();
            }
        }

        private void SyncCurrency()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<IApplicationService>();
                this.syncManager = new NewFoxbitSyncManager(services);
                this.syncManager.SyncCurrencies();
            }
        }

        private void SyncOperations(object? state)
        {
            if (!isBasicsSynced)
            {
                return;
            }
            this.SyncCandlestick();
            this.SyncUserAccount();
            this.SyncOrders();
            this.StartNewCircuits();
        }

        private void StartNewCircuits()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<IApplicationService>();
                this.syncManager = new NewFoxbitSyncManager(services);
                this.syncManager.StartNewCircuits();
            }
        }

        private void SyncOrders()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<IApplicationService>();
                this.syncManager = new NewFoxbitSyncManager(services);
                this.syncManager.SyncOrders();
            }
        }

        private void SyncUserAccount()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<IApplicationService>();
                this.syncManager = new NewFoxbitSyncManager(services);
                this.syncManager.SyncUserAccount();
            }
        }

        private void SyncCandlestick()
        {
            if (this.isRunningCandlestick)
            {
                return;
            }

            this.isRunningCandlestick = true;
            using (var scope = scopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider.GetRequiredService<IApplicationService>();
                this.syncManager = new NewFoxbitSyncManager(services);
                this.syncManager.SyncCandlestick();
            }
            this.isRunningCandlestick = false;
        }
    }
}
