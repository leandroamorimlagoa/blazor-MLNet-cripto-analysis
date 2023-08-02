using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class ForgotPasswordRegisterNewController : BaseController
    {
        public ForgotPasswordRegisterNewController(IApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ForgotPasswordRegisterNewRequest newPassword)
        {
            if (newPassword == null
                || string.IsNullOrEmpty(newPassword.Hash)
                || string.IsNullOrEmpty(newPassword.Password))
            {
                return BadRequest(newPassword);
            }

            CryptoHelper.DecryptWithSecret(newPassword.Hash, out var rollingHash, out var key);
            var newPasswordHash = CryptoHelper.Decrypt(newPassword.Password, key);
            var user = await this.ApplicationService.UserService.GetUserByRollingHash(rollingHash);
            if (user == null)
            {
                return BadRequest(newPassword);
            }

            var cypheredPass = CryptoHelper.Encrypt(newPasswordHash, user.EmailHash);

            user.NewRollingHash();
            user.Password = cypheredPass;

            await this.ApplicationService.UserService.Update(user);

            return Ok();
        }
    }
}
