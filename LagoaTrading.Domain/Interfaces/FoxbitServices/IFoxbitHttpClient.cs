namespace LagoaTrading.Domain.Interfaces.FoxbitServices
{
    public interface IFoxbitHttpClient
    {
        Task<Response> Get<Response>(IBaseRequest request);

        Task<Response> Post<Response>(IBaseRequest request);
    }
}
