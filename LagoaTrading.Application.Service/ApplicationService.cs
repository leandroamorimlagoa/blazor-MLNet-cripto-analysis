using LagoaTrading.Domain.Interfaces.ApplicationServices;

namespace LagoaTrading.Application.Service
{
    public class ApplicationService : IApplicationService
    {
        public IAnalysisService AnalysisService { get; }
        public ICandlestickService CandlestickService { get; }
        public ICircuitService CircuitService { get; }
        public ICurrencyService CurrencyService { get; }
        public IFoxbitService FoxbitService { get; }
        public IInviteService InviteService { get; set; }
        public IMarketService MarketService { get; }
        public IPositionService PositionService { get; }
        public ISimulationService SimulationService { get; }
        public ISyncControlService SyncControlService { get; }
        public IUserService UserService { get; }
        public IUserAccountService UserAccountService { get; }
        public ISecurityService SecurityService { get; }

        public ApplicationService(ISecurityService securityService,
                                    IUserService userService,
                                    IMarketService marketService,
                                    IAnalysisService analysisService,
                                    ICircuitService circuitService,
                                    IInviteService inviteService,
                                    IFoxbitService foxbitService,
                                    ISimulationService simulationService,
                                    IUserAccountService userAccountService,
                                    IPositionService positionService,
                                    ISyncControlService syncControlService,
                                    ICurrencyService currencyService,
                                    ICandlestickService candlestickService)
        {
            SecurityService = securityService;
            UserService = userService;
            MarketService = marketService;
            AnalysisService = analysisService;
            CircuitService = circuitService;
            InviteService = inviteService;
            FoxbitService = foxbitService;
            SimulationService = simulationService;
            UserAccountService = userAccountService;
            PositionService = positionService;
            SyncControlService = syncControlService;
            CurrencyService = currencyService;
            CandlestickService = candlestickService;
        }
    }
}