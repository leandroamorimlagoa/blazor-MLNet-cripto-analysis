using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using LagoaTrading.Domain.Core.Tools;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.FoxbitServices;
using LagoaTrading.Domain.Objects.FoxbitObjects.Requests;
using LagoaTrading.Domain.Objects.FoxbitObjects.Responses;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Data.Service.Implementations
{
    public class FoxbitRepository : IFoxbitRepository
    {
        private readonly IFoxbitHttpClient httpClient;
        private SemaphoreSlim candlestickSemaphore = new SemaphoreSlim(15);
        private SemaphoreSlim orderInfoSemaphore = new SemaphoreSlim(30);

        public FoxbitRepository(IFoxbitHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<ResponseCreateOrder> CreateOrder(RequestCreateOrder requestNewOrder)
            => null;
        //=> httpClient.Post<ResponseCreateOrder>(requestNewOrder);

        public async Task<IEnumerable<ResponseMemberAccount>> RequestMemberAccount(string apiKey, string apiSecret)
        {
            var response = await this.httpClient.Get<MemberAccountDataSet>(new RequestMemberAccount(apiKey, apiSecret));
            if (response == null)
            {
                return null;
            }
            var list = response.Data.Where(x => x.Balance > 0 || x.BalanceAvailable > 0 || x.BalanceBlocked > 0)
                .ToList();
            return list;
        }

        public async Task<ResponseQuote> RequestQuote(Side side, string baseSymbol, string quoteSymbol, decimal value)
        => await this.httpClient.Get<ResponseQuote>(new RequestQuote(side, baseSymbol, quoteSymbol, value));

        public Task<ResponseUserInfo> RequestUserInfo(string apiKey, string apiSecret)
        => this.httpClient.Get<ResponseUserInfo>(new RequestUserInfo(apiKey, apiSecret));

        public async Task<IEnumerable<ResponseCurrencies>> GetCurrencies()
        {
            var currencies = await this.httpClient.Get<ResponseCurrenciesDataSet>(new RequestCurrencies());
            if (currencies == null)
            {
                return null;
            }
            return currencies.Data;
        }

        public async Task<IEnumerable<ResponseMarkets>> GetMarkets()
        {
            var markets = await this.httpClient.Get<MarketsDataSet>(new RequestMarkets());
            if (markets == null)
            {
                return null;
            }
            return markets.Data;
        }

        public async Task<ResponseOrderStatusByID> RequestPosition(string apiKey, string apiSecret, long orderId)
        {
            try
            {
                await orderInfoSemaphore.WaitAsync();
                var request = new RequestOrderStatusByID(apiKey, apiSecret, orderId.ToString());
                return await this.httpClient.Get<ResponseOrderStatusByID>(request);
            }
            finally
            {
                await Task.Delay(2000).ContinueWith(_ => orderInfoSemaphore.Release());
            }
        }

        public async Task<IEnumerable<Candlestick>> GetMarketCandlesticks(DateTime lastSync, Market market)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await candlestickSemaphore.WaitAsync().ConfigureAwait(false);

                var request = new RequestCandlesticks(market.Symbol, lastSync);
                var candlestickResponse = await this.httpClient.Get<List<object>>(request).ConfigureAwait(false);
                sw.Stop();
                if (candlestickResponse == null)
                {
                    return Enumerable.Empty<Candlestick>();
                }

                var candlesticks = new ConcurrentBag<Candlestick>();

                Parallel.ForEach(candlestickResponse, item =>
                {
                    var values = ((IEnumerable)item).Cast<object>()
                               .Select(x => x == null ? x : x.ToString())
                               .ToArray();

                    var dateTime = Convert.ToInt64(values[0].ToString());
                    var candlestick = new Candlestick()
                    {
                        MarketId = market.Id,
                        DateTimeUTC = dateTime,
                        DateTime = LagoaTradingHelper.GetDateTimeFromUTC(dateTime),
                        PriceOpen = Convert.ToDecimal(values[1].ToString()),
                        PriceHighest = Convert.ToDecimal(values[2].ToString()),
                        PriceLowest = Convert.ToDecimal(values[3].ToString()),
                        PriceClose = Convert.ToDecimal(values[4].ToString()),
                        Volume = Convert.ToDecimal(values[5].ToString())
                    };

                    candlesticks.Add(candlestick);
                });

                return candlesticks;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                var timeToWait = 2010 - sw.Elapsed.Milliseconds;
                if (timeToWait > 0)
                {
                    await Task.Delay(timeToWait).ConfigureAwait(false);
                }
                candlestickSemaphore.Release();
            }
        }
    }
}
