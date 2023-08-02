using System.Net.Mail;

namespace LagoaTrading.Domain.Interfaces.EmailServices
{
    public interface IEmailBaseTemplate
    {
        string GetSubject();
        List<MailAddress> GetDestinations();
        string GetBody();
    }
}
