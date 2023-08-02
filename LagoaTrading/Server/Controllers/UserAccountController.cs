using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class UserAccountController : BaseController
    {
        public UserAccountController(IApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpGet("{hash}")]
        public async Task<IActionResult> Get(string hash)
        {
            var accounts = await this.ApplicationService.UserAccountService.GetAccountsByHash(hash);
            return Ok(accounts);
        }
    }
}
