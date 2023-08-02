using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly IApplicationService ApplicationService;

        public AnalysisController(IApplicationService applicationService)
        {
            this.ApplicationService = applicationService;
        }

        [HttpGet("{hash}")]
        public async Task<IActionResult> Get(string hash)
        {
            CryptoHelper.DecryptWithSecret(hash, out string userHash, out string key);

            var analysisResult = await this.ApplicationService.AnalysisService.GetQuickAnalysis(userHash);

            if (analysisResult == default)
            {
                return BadRequest(hash);
            }
            var list = analysisResult.Select(a => new AnalysisResponse
            {
                AvgPrice = a.AvgPrice,
                EndPriceClose = a.EndPriceClose,
                MarketId = a.MarketId,
                MaxPriceHighest = a.MaxPriceHighest,
                MinPriceLowest = a.MinPriceLowest,
                StartPriceOpen = a.StartPriceOpen,
                ResultPrice = a.ResultPrice,
                SumVolume = a.SumVolume,
                Symbol = a.Symbol.ToUpper()
            }).ToList();

            return Ok(list);
        }
    }
}
