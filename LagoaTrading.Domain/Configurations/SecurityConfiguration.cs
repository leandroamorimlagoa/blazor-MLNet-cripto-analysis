namespace LagoaTrading.Domain.Configurations
{
    public class SecurityConfiguration
    {
        public string ConnectionString { get; set; }
        public string UrlBaseFoxbit { get; set; }
        public string NewUserActivationFullUrl { get; set; }
        public string NewPasswordForgotFullUrl { get; set; }
        public EmailServer EmailServer { get; set; }
    }
}
