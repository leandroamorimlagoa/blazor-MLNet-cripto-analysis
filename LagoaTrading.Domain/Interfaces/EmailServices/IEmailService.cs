namespace LagoaTrading.Domain.Interfaces.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(IEmailBaseTemplate email);
    }
}
