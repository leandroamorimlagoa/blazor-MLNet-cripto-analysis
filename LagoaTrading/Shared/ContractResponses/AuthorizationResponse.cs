namespace LagoaTrading.Shared.ContractResponses
{
    public class AuthorizationResponse
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public DateTime ExpireIn { get; set; }
    }
}
