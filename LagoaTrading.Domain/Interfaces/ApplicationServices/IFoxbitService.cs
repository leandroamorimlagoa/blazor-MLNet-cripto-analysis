using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface IFoxbitService
    {
        Task<IEnumerable<ResponseCurrencies>> GetCurrencies();
        Task<IEnumerable<Candlestick>> GetMarketCandlesticks(DateTime lastSync, Market market);
        Task<IEnumerable<ResponseMarkets>> GetMarkets();
        Task<IEnumerable<ResponseMemberAccount>> RequestMemberAccount(string apiKey, string apiSecret);
        Task<ResponseOrderStatusByID> RequestPosition(string apiKey, string apiSecret, long orderId);
        Task<ResponseUserInfo> RequestUserInfo(string apiKey, string apiSecret);
    }
}
