using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Server.Core.Basics;
using LagoaTrading.Server.IntegrationManagers;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.Core;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandlesticksController : BaseController
    {
        public CandlesticksController(IApplicationService applicationService) : base(applicationService)
        {
        }

        [HttpGet("LastUpdate")]
        public async Task<IActionResult> Get()
        {
            var sync = await this.ApplicationService.SyncControlService.GetByName(Constants.SyncControlNames.CandlesticksName);
            var result = new LastUpdateResponse()
            {
                LastUpdated = sync == null ? DateTime.MinValue.AddMicroseconds(1) : sync.LastSync,
                Name = Constants.SyncControlNames.CandlesticksName
            };
            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Post()
        {
            var sync = new SyncCandlestick(this.ApplicationService);
            await sync.Execute();
            return Ok();
        }

        [HttpPost("GenerateFile")]
        public async Task<IActionResult> GenerateFile()
        {
            await this.ApplicationService.CandlestickService.GenerateFile();
            return Ok();
        }
    }
}
