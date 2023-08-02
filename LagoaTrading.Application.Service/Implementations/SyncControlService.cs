using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.Repositories;

namespace LagoaTrading.Application.Service.Implementations
{
    public class SyncControlService : ISyncControlService
    {
        private readonly IApplicationRepositories applicationRepositories;

        public SyncControlService(IApplicationRepositories applicationRepositories)
        {
            this.applicationRepositories = applicationRepositories;
        }

        public Task<SyncControl?> GetByName(string name)
        => this.applicationRepositories.SyncControlRepository.GetByName(name);

        public Task<DateTime> GetLastSync(string syncName)
        => this.applicationRepositories.SyncControlRepository.GetLastSync(syncName);

        public Task Save(string candlesticksName, DateTime now)
        => this.applicationRepositories.SyncControlRepository.Save(candlesticksName, now);

        public Task<bool> ShouldSyncNow(string syncName)
        => this.applicationRepositories.SyncControlRepository.ShouldSyncNow(syncName);
    }
}
