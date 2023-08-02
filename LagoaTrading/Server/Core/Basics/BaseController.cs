using LagoaTrading.Domain.Interfaces.ApplicationServices;
using Microsoft.AspNetCore.Mvc;

namespace LagoaTrading.Server.Core.Basics
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IApplicationService ApplicationService;

        public BaseController(IApplicationService applicationService)
        {
            this.ApplicationService = applicationService;
        }
    }
}
