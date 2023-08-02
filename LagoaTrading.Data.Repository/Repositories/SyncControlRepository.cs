using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.Core;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class SyncControlRepository : ISyncControlRepository
    {
        private readonly LagoaTradingContext context;
        private readonly LagoaTradingConfiguration configuration;

        public SyncControlRepository(LagoaTradingContext context,
                                        LagoaTradingConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public Task<SyncControl?> GetByName(string name)
        => this.context.SyncControl.FirstOrDefaultAsync(s => s.Name == name);

        public async Task<DateTime> GetLastSync(string candlesticksName)
        {
            var lastSync = await this.context.SyncControl
                                                .Where(s => s.Name == candlesticksName)
                                                .Select(s => s.LastSync)
                                                .FirstOrDefaultAsync();

            if (lastSync == default || lastSync == DateTime.MinValue)
            {
                lastSync = DateTime.UtcNow.AddYears(-1);
            }

            return lastSync;
        }

        public async Task Save(string candlesticksName, DateTime now)
        {
            var syncControl = await this.context.SyncControl.FirstOrDefaultAsync(s => s.Name == candlesticksName);
            if (syncControl == null)
            {
                syncControl = new SyncControl() { Name = candlesticksName, LastSync = now };
                await this.context.SyncControl.AddAsync(syncControl);
            }
            else
            {
                syncControl.LastSync = now;
                this.context.SyncControl.Update(syncControl);
            }
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> ShouldSyncNow(string syncName)
        {
            var interfalInMinutes = this.GetInterval(syncName);
            var lastSync = await this.context.SyncControl
                                        .Where(s => s.Name == syncName)
                                        .Select(s => s.LastSync)
                                        .FirstOrDefaultAsync();

            if (lastSync == default || lastSync == DateTime.MinValue)
            {
                return true;
            }
            return lastSync.AddMinutes(interfalInMinutes) < DateTime.UtcNow;
        }

        private int GetInterval(string syncName)
        {
            if (syncName == Constants.SyncControlNames.CandlesticksName)
            {
                return this.configuration.Sync.OperationsIntervalMinutes;
            }
            return this.configuration.Sync.DefaultIntervalMinutes;
        }
    }
}