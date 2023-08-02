using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Server.Core.Securities;
using LagoaTrading.Shared.ContractResponses;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class ActivateUserController : BaseController
    {
        public ActivateUserController(IApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpGet("{hash}")]
        public async Task<IActionResult> ActivateUser([FromRoute] string hash)
        {
            if (string.IsNullOrEmpty(hash))
            {
                return BadRequest(hash);
            }

            var user = await ApplicationService.UserService.ActivateUser(hash);
            if (user == null)
            {
                return BadRequest(user);
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
