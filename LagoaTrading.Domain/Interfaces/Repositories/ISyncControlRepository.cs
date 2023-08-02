using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface ISyncControlRepository
    {
        Task<SyncControl?> GetByName(string syncName);
        Task<DateTime> GetLastSync(string syncName);
        Task Save(string syncName, DateTime now);
        Task<bool> ShouldSyncNow(string syncName);
    }
}
