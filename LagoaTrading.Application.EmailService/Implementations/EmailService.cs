using System.Net;
using System.Net.Mail;
using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Interfaces.EmailServices;

namespace LagoaTrading.Application.EmailService.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly LagoaTradingConfiguration configuration;

        public EmailService(LagoaTradingConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(IEmailBaseTemplate email)
        {
            var emailServer = configuration.Security.EmailServer;
            var smtpClient = new SmtpClient(emailServer.SmtpHost)
            {
                Port = emailServer.SmtpPort,
                UseDefaultCredentials = emailServer.SmtpUseDefaultCredentials,
                Credentials = new NetworkCredential(emailServer.SmtpUser, emailServer.SmtpPassword),
                EnableSsl = emailServer.SmtpUseSsl,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailServer.SmtpFromAddress, emailServer.SmtpFromName),
                Subject = email.GetSubject(),
                Body = email.GetBody(),
                IsBodyHtml = true,
            };

            mailMessage.ReplyToList.Add(new MailAddress(emailServer.SmtpReplyToAddress, emailServer.SmtpReplyToName));

            var listTo = email.GetDestinations();
            foreach (var dest in listTo)
            {
                mailMessage.To.Add(dest);
            }

            smtpClient.Send(mailMessage);
        }
    }
}