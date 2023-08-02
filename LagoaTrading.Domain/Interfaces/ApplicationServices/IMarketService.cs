using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface IMarketService
    {
        Task<Market?> Get(int marketid);
        Task<IEnumerable<Market>> GetAll();
        Task<Market?> GetBySymbol(string symbol);
        Task Save(Market market);
    }
}
