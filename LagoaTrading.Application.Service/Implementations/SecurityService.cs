using LagoaTrading.Application.EmailService.EmailModels;
using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.EmailServices;
using LagoaTrading.Domain.Interfaces.Repositories;

namespace LagoaTrading.Application.Service.Implementations
{
    public class SecurityService : ISecurityService
    {
        private readonly IApplicationRepositories repositories;
        private readonly IEmailService emailService;
        private readonly LagoaTradingConfiguration configuration;

        public SecurityService(IApplicationRepositories repositories,
                                IEmailService emailService,
                                LagoaTradingConfiguration configuration)
        {
            this.repositories = repositories;
            this.emailService = emailService;
            this.configuration = configuration;
        }

        public async Task<User> AuthenticateUser(string emailHash, string passHash)
        {
            var user = await this.repositories.UserRepository.GetUser(emailHash, passHash);
            if(user == default)
            {
                return default;
            }
            user.NewRollingHash();
            await this.repositories.UserRepository.Update(user);

            return user;
        }

        public async Task<bool> RequestNewPassword(string passwordHash)
        {
            var user = await this.repositories.UserRepository.GetUserByEmailHash(passwordHash);
            if(user == default)
            {
                return false;
            }
            user.NewRollingHash();
            await this.repositories.UserRepository.Update(user);

            var email = new ForgotPasswordMailMessage(user.Email, user.RollingHash, this.configuration.Security.NewPasswordForgotFullUrl);
            this.emailService.SendEmail(email);
            return true;
        }
    }
}
