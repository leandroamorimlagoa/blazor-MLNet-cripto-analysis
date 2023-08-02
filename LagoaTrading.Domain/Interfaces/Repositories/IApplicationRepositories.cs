using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface IApplicationRepositories
    {
        IAnalysisRepository AnalysisRepository { get; }
        ICandlestickRepository CandlestickRepository { get; }
        ICircuitRepository CircuitRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        IInviteRepository InviteRepository { get; }
        IMarketRepository MarketRepository { get; }
        IPositionRepository PositionRepository { get; }
        ISimulationRepository SimulationRepository { get; }
        ISyncControlRepository SyncControlRepository { get; }
        IUserRepository UserRepository { get; }
        IUserAccountRepository UserAccountRepository { get; }
    }
}
