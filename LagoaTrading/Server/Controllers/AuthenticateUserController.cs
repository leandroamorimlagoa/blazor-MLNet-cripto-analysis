using LagoaTrading.Application.Service;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Server.Core.Securities;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class AuthenticateUserController : BaseController
    {
        public AuthenticateUserController(ApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthenticateUserRequest request)
        {
            if (request == null
                || string.IsNullOrEmpty(request.Hash)
                || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(request);
            }

            CryptoHelper.DecryptWithSecret(request.Hash, out var EmailHash, out var key);
            var passwordHash = CryptoHelper.Decrypt(request.Password, key);
            var encryptedPassword = CryptoHelper.Encrypt(passwordHash, EmailHash);

            var user = await this.ApplicationService.SecurityService.AuthenticateUser(EmailHash, encryptedPassword);
            if (user == default)
            {
                return BadRequest(request);
            }

            var result = new AuthorizationResponse
            {
                Name = user.Name,
                Token = SecurityManager.GenerateToken(user),
                ExpireIn = DateTime.Now.Date.AddDays(30)
            };
            return Ok(result);
        }
    }
}
