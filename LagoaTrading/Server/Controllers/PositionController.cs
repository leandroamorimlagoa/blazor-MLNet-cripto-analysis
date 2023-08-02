using LagoaTrading.Application.Service;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    public class PositionController : BaseController
    {
        public PositionController(IApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpPost("{positionId}")]
        public async Task<IActionResult> CreatePosition(long positionId)
        {
            var position = await this.ApplicationService.PositionService.GetPositionById(positionId);
            if (position == null)
            {
                return this.BadRequest();
            }

            await ApplicationService.CircuitService.CreatePositionToSell(position.CircuitId.Value, position);
            return Ok();
        }
    }
}
