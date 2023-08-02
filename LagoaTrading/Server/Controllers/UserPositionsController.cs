using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class UserPositionsController : BaseController
    {
        public UserPositionsController(IApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserPositionsRequest query)
        {
            if (query == null
                || string.IsNullOrEmpty(query.Hash)
                || query.Take <= 0)
            {
                return BadRequest(query);
            }

            CryptoHelper.DecryptWithSecret(query.Hash, out string userHash, out string key);

            var user = await this.ApplicationService.UserService.GetUserByRollingHash(userHash);
            if (user == default)
            {
                return BadRequest(query.Hash);
            }
            var positions = (await this.ApplicationService.PositionService.GetPositionsByUser(user.Id, query));
            var result = positions.Select(p => new UserPositionsResponse
            {
                Symbol = p.Market.CurrencyBase.Name,
                Side = p.Side,
                QuantityExecuted = p.QuantityExecuted + p.Quantity,
                Total = p.QuantityExecuted * p.UnitPrice,
                Price = p.UnitPrice,
                State = p.State,
                CreatedAt = p.CreatedAt
            });
            return Ok(result);
        }
    }
}
