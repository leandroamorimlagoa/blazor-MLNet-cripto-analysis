using System.Net.Mail;
using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Core.Securities;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.EmailServices;

namespace LagoaTrading.Application.EmailService.EmailModels
{
    public class NewUserActivationMailMessage : IEmailBaseTemplate
    {
        public User To { get; set; }

        private string BodyTemplateName = "new-user-activation-message-welcome.html";
        private readonly LagoaTradingConfiguration configuration;

        public NewUserActivationMailMessage(LagoaTradingConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetSubject()
        => "Lagoa Trading - Ativação de conta";

        public List<MailAddress> GetDestinations()
        => new List<MailAddress>() { new MailAddress(To.Email) };

        public string GetBody()
        {
            string body = EmailTemplateBody.LoadTemplate($"Templates\\{BodyTemplateName}");

            body = body.Replace(ConstantNamesServer.InterpolationNames.activation_url, configuration.Security.NewUserActivationFullUrl)
                        .Replace(ConstantNamesServer.InterpolationNames.hash, To.RollingHash);
            return body;

        }
    }
}
