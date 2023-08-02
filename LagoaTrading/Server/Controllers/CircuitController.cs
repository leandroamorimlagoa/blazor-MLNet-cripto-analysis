using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Server.IntegrationManagers;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class CircuitController : BaseController
    {
        public CircuitController(IApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CircuitRequest query)
        {
            if (query == null
                || string.IsNullOrEmpty(query.Hash)
                || query.Take <= 0)
            {
                return BadRequest();
            }

            CryptoHelper.DecryptWithSecret(query.Hash, out string rollingHash, out string key);
            (User user, Parameter parameter) = await this.ApplicationService.UserService.GetUserByRollingHashWithParameter(rollingHash);
            if (user == default)
            {
                return BadRequest(query.Hash);
            }
            var list = (await this.ApplicationService.CircuitService.GetCircuitList(user, query))
                                        .Select(c => new CircuitResponse
                                        {
                                            DurationHours = c.DurationHours,
                                            Profit = c.Profit,
                                            PositionBuy = c.PositionBuy,
                                            PositionSell = c.PositionSell,
                                        });

            return Ok(list);
        }

        [HttpPost("{hash}/{circuitType}")]
        public async Task<IActionResult> Post(string hash, CircuitType circuitType)
        {
            if (string.IsNullOrEmpty(hash)
                || circuitType == null)
            {
                return BadRequest();
            }

            CryptoHelper.DecryptWithSecret(hash, out string rollingHash, out string key);
            (User user, Parameter parameter) = await this.ApplicationService.UserService.GetUserByRollingHashWithParameter(rollingHash);
            if (user == default || parameter == null)
            {
                return BadRequest(hash);
            }

            var sync = new SyncCandlestick(this.ApplicationService);
            await sync.Execute();

            var result = await this.ApplicationService.CircuitService.StartCircuit(user, parameter, circuitType);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
