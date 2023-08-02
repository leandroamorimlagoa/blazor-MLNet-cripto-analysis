using System.Net.Http.Json;
using System.Text;
using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Interfaces.FoxbitServices;
using LagoaTrading.Shared.Extensions;
using LagoaTrading.Shared.Objects;
using Newtonsoft.Json;

namespace LagoaTrading.Data.Service.HttpClients
{
    public class FoxbitHttpClient : HttpClient, IFoxbitHttpClient
    {
        private readonly string accessKeyHeaderName = "X-FB-ACCESS-KEY";
        private readonly string timestampHeaderName = "X-FB-ACCESS-TIMESTAMP";
        private readonly string signatureHeaderName = "X-FB-ACCESS-SIGNATURE";

        private HttpClient httpClient;

        public FoxbitHttpClient(LagoaTradingConfiguration configuration)
        {
            var handler = new HttpClientHandler();
            handler.Proxy = null;
            handler.UseProxy = false;
            this.httpClient = new HttpClient(handler) { BaseAddress = new Uri(configuration.Security.UrlBaseFoxbit) };
        }

        public async Task<Response> Get<Response>(IBaseRequest request)
        {
            try
            {
                var payload = new PayloadSignature
                {
                    method = "GET",
                    url = request.GetUrl(),
                    query = request.GetQuery()
                };

                if (request is IRestrictRequest)
                {
                    var requestHeader = (IRestrictRequest)request;
                    payload.ApiSecret = requestHeader.ApiSecret;
                    payload.AccessKey = requestHeader.ApiKey;
                    SetupHttpClient(payload);
                }

                var fullUrl = "";
                if (!string.IsNullOrEmpty(payload.query))
                    fullUrl = $"{payload.url}?{payload.query}";
                else
                    fullUrl = payload.url;

                var response = await this.httpClient.GetAsync(fullUrl);
                if (!response.IsSuccessStatusCode)
                {
                    // TODO: Fazer a desativação do usuário para o caso da Api Secret e Api Key não estarem corretas
                    return default;
                }
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response>(content);
                return result;
            }
            catch (Exception err)
            {
                // TODO: Tratamento de erro ao fazer o request para Foxbit. Log?
            }
            return default;
        }

        public async Task<Response> Post<Response>(IBaseRequest request)
        {
            var payload = new PayloadSignature
            {
                method = "POST",
                url = request.GetUrl(),
                body = JsonConvert.SerializeObject(request),
                query = request.GetQuery()
            };

            if (request is IRestrictRequest)
            {
                var requestHeader = (IRestrictRequest)request;
                payload.ApiSecret = requestHeader.ApiSecret;
                payload.AccessKey = requestHeader.ApiKey;
                SetupHttpClient(payload);
            }

            var content = new StringContent(payload.body, Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync(payload.url, content);
            if (!response.IsSuccessStatusCode)
            {
                var error = "";
                if(response.Content.ReadAsStream() != null)
                    error = await response.Content.ReadAsStringAsync();
                
                return default;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(responseContent);
            return result;
        }

        private void SetupHttpClient(PayloadSignature payload)
        {
            try
            {
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
                if (!string.IsNullOrEmpty(payload.query))
                {
                    payload.query = payload.query.StartsWith("?") ? payload.query.Substring(1) : payload.query;
                }

                string prehash = $"{timestamp}{payload.method}{payload.url}";
                if (!string.IsNullOrEmpty(payload.query))
                {
                    prehash += $"{payload.query}";
                }

                if (!string.IsNullOrEmpty(payload.body))
                {
                    prehash += $"{payload.body}";
                }

                string signature = CryptoHelper.Create256(prehash, payload.ApiSecret).ToLower();

                if (httpClient.DefaultRequestHeaders.Contains(accessKeyHeaderName))
                    httpClient.DefaultRequestHeaders.Remove(accessKeyHeaderName);

                if (httpClient.DefaultRequestHeaders.Contains(timestampHeaderName))
                    httpClient.DefaultRequestHeaders.Remove(timestampHeaderName);

                if (httpClient.DefaultRequestHeaders.Contains(signatureHeaderName))
                    httpClient.DefaultRequestHeaders.Remove(signatureHeaderName);

                httpClient.DefaultRequestHeaders.Add(accessKeyHeaderName, payload.AccessKey);
                httpClient.DefaultRequestHeaders.Add(timestampHeaderName, timestamp);
                httpClient.DefaultRequestHeaders.Add(signatureHeaderName, signature);
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
