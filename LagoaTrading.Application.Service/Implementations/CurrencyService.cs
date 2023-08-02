using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.Repositories;

namespace LagoaTrading.Application.Service.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IApplicationRepositories repositories;

        public CurrencyService(IApplicationRepositories repositories)
        {
            this.repositories = repositories;
        }

        public Task<IEnumerable<Currency>> GetAll()
        => this.repositories.CurrencyRepository.GetAll();

        public Task Save(Currency currency)
        => this.repositories.CurrencyRepository.Save(currency);
    }
}
