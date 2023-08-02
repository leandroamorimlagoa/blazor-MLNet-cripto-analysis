using Newtonsoft.Json;

namespace LagoaTrading.Domain.Interfaces.FoxbitServices
{
    public interface IRestrictRequest : IBaseRequest
    {
        [JsonIgnore]
        public string ApiKey { get; set; }
        [JsonIgnore]
        public string ApiSecret { get; set; }
    }
}
