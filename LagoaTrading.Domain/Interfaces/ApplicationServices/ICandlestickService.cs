using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface ICandlestickService
    {
        Task GenerateFile();
        Task Save(IEnumerable<Candlestick> candlesticks);
    }
}
