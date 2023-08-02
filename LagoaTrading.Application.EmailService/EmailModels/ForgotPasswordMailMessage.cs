using System.Net.Mail;
using LagoaTrading.Domain.Core.Securities;
using LagoaTrading.Domain.Interfaces.EmailServices;

namespace LagoaTrading.Application.EmailService.EmailModels
{
    public class ForgotPasswordMailMessage : IEmailBaseTemplate
    {
        private readonly string email;
        private readonly string url;
        private readonly string bodyTemplateName = "forgot-my-password.html";
        private readonly string hash;

        public ForgotPasswordMailMessage(string email, string hash, string url)
        {
            this.email = email;
            this.hash = hash;
            this.url = url;
        }

        public string GetSubject()
        => "Lagoa Trading - Recuperação de senha";

        public List<MailAddress> GetDestinations()
        => new List<MailAddress>() { new MailAddress(email) };

        public string GetBody()
        {
            string body = EmailTemplateBody.LoadTemplate($"Templates\\{bodyTemplateName}")
                                            .Replace(ConstantNamesServer.InterpolationNames.forgot_password_url, url)
                                            .Replace(ConstantNamesServer.InterpolationNames.hash, this.hash);
            return body;
        }

    }
}
