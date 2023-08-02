using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface IMarketRepository
    {
        Task<Market?> Get(long marketId);
        Task<IEnumerable<Market>> GetAll();
        Task<Market?> GetBySymbol(string symbol);
        Task Save(Market market);
    }
}
