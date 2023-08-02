using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.Objects;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface ICandlestickRepository
    {
        Task<IEnumerable<CandlestickJsonObject>> GetAll();
        Task Save(IEnumerable<Candlestick> candlesticks);
    }
}
