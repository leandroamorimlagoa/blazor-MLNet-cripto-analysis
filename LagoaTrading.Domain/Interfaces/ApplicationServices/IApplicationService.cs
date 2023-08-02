namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface IApplicationService
    {
        IAnalysisService AnalysisService { get; }
        ICandlestickService CandlestickService { get; }
        ICircuitService CircuitService { get; }
        ICurrencyService CurrencyService { get; }
        IFoxbitService FoxbitService { get; }
        IInviteService InviteService { get; }
        IMarketService MarketService { get; }
        IPositionService PositionService { get; }
        ISimulationService SimulationService { get; }
        ISyncControlService SyncControlService { get; }
        IUserService UserService { get; }
        IUserAccountService UserAccountService { get; }
        ISecurityService SecurityService { get; }
    }
}
