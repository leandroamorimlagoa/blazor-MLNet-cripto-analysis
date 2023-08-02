using LagoaTrading.Application.EmailService.EmailModels;
using LagoaTrading.Domain.Configurations;
using LagoaTrading.Domain.Core.Securities;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.EmailServices;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class NewUserController : BaseController
    {
        private readonly LagoaTradingConfiguration configuration;
        private readonly IEmailService emailService;

        public NewUserController(IApplicationService applicationService,
                                    IEmailService emailHandler,
                                    LagoaTradingConfiguration configuration) : base(applicationService)
        {
            this.emailService = emailHandler;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewUserRequest newUser)
        {
            if (newUser == null
                || string.IsNullOrEmpty(newUser.ApiKey)
                || string.IsNullOrEmpty(newUser.ApiSecret)
                || string.IsNullOrEmpty(newUser.InviteCode)
                || string.IsNullOrEmpty(newUser.UserName)
                || string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest(newUser);
            }

            var invite = await this.ApplicationService.InviteService.Get(newUser.InviteCode);
            if (invite == null)
            {
                return BadRequest("Invalid invite code");
            }

            CryptoHelper.DecryptWithSecret(newUser.Password, out var passwordHash, out var key);
            var apiKey = CryptoHelper.Decrypt(newUser.ApiKey, key);
            var apiSecret = CryptoHelper.Decrypt(newUser.ApiSecret, key);

            var foxbitUser = await this.ApplicationService.FoxbitService.RequestUserInfo(apiKey, apiSecret);
            if (foxbitUser == null || foxbitUser.Disabled)
            {
                return BadRequest(foxbitUser);
            }

            var parameter = await this.ApplicationService.UserService.GetParameterByApiKey(apiKey, apiSecret);
            if (parameter != null)
            {
                return BadRequest(parameter);
            }

            var user = await this.ApplicationService.UserService.GetByEmail(foxbitUser.Email);
            if (user != null)
            {
                return BadRequest(user);
            }

            user = new User()
            {
                Email = foxbitUser.Email,
                Name = newUser.UserName,
                EmailHash = CryptoHelper.Create256(foxbitUser.Email),
                Identifier = foxbitUser.Identifier,
                CreatedAt = DateTime.UtcNow,
                Status = UserStatus.WaitingForActivation
            };
            user.NewRollingHash();
            user.Password = CryptoHelper.Encrypt(passwordHash, user.EmailHash);

            await this.ApplicationService.UserService.Add(user);
            invite.UsedAt = DateTime.UtcNow;
            invite.ToUserId = user.Id;
            this.ApplicationService.InviteService.Update(invite);

            var newApiKey = CryptoHelper.Encrypt(apiKey, user.EmailHash);
            var newApiSecret = CryptoHelper.Encrypt(apiSecret, user.EmailHash);
            var accountBalanceCurrency = await this.GetAccountBalance(apiKey, apiSecret, ConstantNamesServer.Currency.BrlSymbol);

            var parametro = new Parameter
            {
                UserId = user.Id,
                ApiKey = newApiKey,
                ApiSecret = newApiSecret,
                AccountBalanceCurrency = accountBalanceCurrency,
                ReferenceAbsoluteValue = accountBalanceCurrency,
                AvaliableValue = accountBalanceCurrency,
            };
            await this.ApplicationService.UserService.SaveParameter(parametro);

            var mail = new NewUserActivationMailMessage(this.configuration) { To = user };
            this.emailService.SendEmail(mail);

            var msg = ConstantNamesServer.Messages.ActivationEmailSent.Replace(ConstantNamesServer.InterpolationNames.email, user.Email);

            var response = new NewUserResponse { Email = user.Email, Message = msg };
            return Ok(response);
        }

        private async Task<decimal> GetAccountBalance(string apiKey, string apiSecret, string symbol)
        {
            var result = await this.ApplicationService.FoxbitService.RequestMemberAccount(apiKey, apiSecret);
            var accountBalance = result.FirstOrDefault(x => x.CurrencySymbol == symbol);
            if (accountBalance == null)
            {
                return 0;
            }
            return accountBalance.Balance;
        }
    }
}
