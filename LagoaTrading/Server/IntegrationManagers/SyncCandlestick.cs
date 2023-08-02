using System.Diagnostics;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Shared.Core;

namespace LagoaTrading.Server.IntegrationManagers
{
    public class SyncCandlestick : BaseSync
    {
        public SyncCandlestick(IApplicationService applicationService)
                        : base(Constants.SyncControlNames.CandlesticksName, applicationService)
        {
        }

        internal async Task Execute()
        {
            var shouldSync = await this.applicationService.SyncControlService.ShouldSyncNow(Constants.SyncControlNames.CandlesticksName);
            if (!shouldSync)
            {
                return;
            }

            var markets = await this.applicationService.MarketService.GetAll();
            var lastSync = await this.applicationService.SyncControlService.GetLastSync(Constants.SyncControlNames.CandlesticksName);

            var now = DateTime.UtcNow;
            var sw = Stopwatch.StartNew();
            var tasks = new List<Task>();
            foreach (var market in markets)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var candlesticks = await this.applicationService.FoxbitService.GetMarketCandlesticks(lastSync, market);
                    await this.applicationService.CandlestickService.Save(candlesticks);
                }));
            }

            await Task.WhenAll(tasks);

            await this.applicationService.SyncControlService.Save(Constants.SyncControlNames.CandlesticksName, now);
            sw.Stop();
            Console.WriteLine($"====> Candlesticks sync took {sw.Elapsed.TotalSeconds:F2} seconds");
        }
    }
}