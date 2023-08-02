namespace LagoaTrading.Domain.Interfaces.FoxbitServices
{
    public interface IBaseRequest
    {
        /// <summary>
        /// Relative path
        /// </summary>
        /// <returns></returns>
        string GetUrl();

        /// <summary>
        /// Query string
        /// </summary>
        /// <returns></returns>
        string GetQuery();
    }
}
