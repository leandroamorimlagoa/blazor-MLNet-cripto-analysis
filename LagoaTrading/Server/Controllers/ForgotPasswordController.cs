using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class ForgotPasswordController : BaseController
    {
        public ForgotPasswordController(IApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpPost("{hash}")]
        public async Task<IActionResult> Get([FromRoute] string hash)
        {
            if (string.IsNullOrEmpty(hash))
            {
                return BadRequest(hash);
            }

            CryptoHelper.DecryptWithSecret(hash, out var passwordHash, out var key);

            var result = await this.ApplicationService.SecurityService.RequestNewPassword(passwordHash);
            if (!result)
            {
                return BadRequest(hash);
            }
            return Ok();
        }
    }
}
