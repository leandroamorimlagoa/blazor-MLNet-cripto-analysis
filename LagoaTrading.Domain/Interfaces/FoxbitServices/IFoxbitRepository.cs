using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Objects.FoxbitObjects.Requests;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Domain.Interfaces.FoxbitServices
{
    public interface IFoxbitRepository
    {
        Task<ResponseCreateOrder> CreateOrder(RequestCreateOrder requestNewOrder);
        Task<IEnumerable<ResponseCurrencies>> GetCurrencies();
        Task<IEnumerable<Candlestick>> GetMarketCandlesticks(DateTime lastSync, Market market);
        Task<IEnumerable<ResponseMarkets>> GetMarkets();
        Task<IEnumerable<ResponseMemberAccount>> RequestMemberAccount(string apiKey, string apiSecret);
        Task<ResponseOrderStatusByID> RequestPosition(string apiKey, string apiSecret, long orderId);
        Task<ResponseQuote> RequestQuote(Side side, string baseSymbol, string quoteSymbol, decimal value);
        Task<ResponseUserInfo> RequestUserInfo(string apiKey, string apiSecret);
    }
}
