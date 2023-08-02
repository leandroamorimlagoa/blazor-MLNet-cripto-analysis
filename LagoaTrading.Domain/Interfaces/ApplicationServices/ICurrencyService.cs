using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetAll();
        Task Save(Currency item);
    }
}
