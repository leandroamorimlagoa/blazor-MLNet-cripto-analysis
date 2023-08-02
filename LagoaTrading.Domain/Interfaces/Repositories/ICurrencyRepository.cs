using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAll();
        Task Save(Currency currency);
    }
}
