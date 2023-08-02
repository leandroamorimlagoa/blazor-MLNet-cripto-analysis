using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class SimulationController : BaseController
    {
        public SimulationController(IApplicationService applicationService)
            : base(applicationService)
        {
        }

        [HttpGet("{hash}/{marketid}")]
        public async Task<IActionResult> Get(string hash, int marketid)
        {
            CryptoHelper.DecryptWithSecret(hash, out string userHash, out string key);
            (User user, Parameter parameter) = await this.ApplicationService.UserService.GetUserByRollingHashWithParameter(userHash);
            if (user == default
                || parameter == default)
            {
                return BadRequest(hash);
            }

            var market = await this.ApplicationService.MarketService.Get(marketid);
            if (market == default)
            {
                return BadRequest(marketid);
            }

            var simulation = await this.ApplicationService.SimulationService.GetSimulation(parameter, market);
            return Ok(simulation);
        }
    }
}
