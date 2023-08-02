using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.FoxbitServices;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;

namespace LagoaTrading.Application.Service.Implementations
{
    public class FoxbitService : IFoxbitService
    {
        private readonly IFoxbitRepository foxbitRepository;

        public FoxbitService(IFoxbitRepository foxbitRepository)
        {
            this.foxbitRepository = foxbitRepository;
        }

        public Task<IEnumerable<ResponseMemberAccount>> RequestMemberAccount(string apiKey, string apiSecret)
        => this.foxbitRepository.RequestMemberAccount(apiKey, apiSecret);

        public Task<ResponseUserInfo> RequestUserInfo(string apiKey, string apiSecret)
        => this.foxbitRepository.RequestUserInfo(apiKey, apiSecret);

        public Task<IEnumerable<ResponseCurrencies>> GetCurrencies()
        => this.foxbitRepository.GetCurrencies();

        public Task<IEnumerable<ResponseMarkets>> GetMarkets()
        => this.foxbitRepository.GetMarkets();

        public Task<ResponseOrderStatusByID> RequestPosition(string apiKey, string apiSecret, long orderId)
        => this.foxbitRepository.RequestPosition(apiKey, apiSecret, orderId);

        public Task<IEnumerable<Candlestick>> GetMarketCandlesticks(DateTime lastSync, Market market)
        => this.foxbitRepository.GetMarketCandlesticks(lastSync, market);
    }
}
