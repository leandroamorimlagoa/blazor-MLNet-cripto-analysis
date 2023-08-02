using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;

namespace LagoaTrading.Application.Service.Implementations
{
    public class MarketService : IMarketService
    {
        private readonly IApplicationRepositories applicationRepositories;
        public MarketService(IApplicationRepositories applicationRepositories)
        {
            this.applicationRepositories = applicationRepositories;
        }

        public Task<Market?> Get(int marketid)
        => this.applicationRepositories.MarketRepository.Get(marketid);

        public Task<IEnumerable<Market>> GetAll()
        => this.applicationRepositories.MarketRepository.GetAll();

        public Task<Market?> GetBySymbol(string symbol)
        => this.applicationRepositories.MarketRepository.GetBySymbol(symbol);

        public Task Save(Market market)
        => this.applicationRepositories.MarketRepository.Save(market);
    }
}
