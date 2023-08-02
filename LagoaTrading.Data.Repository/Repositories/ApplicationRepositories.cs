using LagoaTrading.Domain.Interfaces.Repositories;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class ApplicationRepositories : IApplicationRepositories
    {
        public IAnalysisRepository AnalysisRepository { get; }
        public ICandlestickRepository CandlestickRepository { get; }
        public ICircuitRepository CircuitRepository { get; }
        public ICurrencyRepository CurrencyRepository { get; }
        public IInviteRepository InviteRepository { get; }
        public IMarketRepository MarketRepository { get; }
        public IPositionRepository PositionRepository { get; }
        public ISimulationRepository SimulationRepository { get; }
        public ISyncControlRepository SyncControlRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserAccountRepository UserAccountRepository { get; }

        public ApplicationRepositories(IUserRepository userRepository,
                                        IMarketRepository marketRepository,
                                        IAnalysisRepository analysisRepository,
                                        ICircuitRepository circuitRepository,
                                        ISimulationRepository simulationRepository,
                                        IInviteRepository inviteRepository,
                                        IUserAccountRepository userAccountRepository,
                                        IPositionRepository positionRepository,
                                        ISyncControlRepository syncControlRepository,
                                        ICurrencyRepository currencyRepository,
                                        ICandlestickRepository candlestickRepository)
        {
            UserRepository = userRepository;
            MarketRepository = marketRepository;
            AnalysisRepository = analysisRepository;
            CircuitRepository = circuitRepository;
            SimulationRepository = simulationRepository;
            InviteRepository = inviteRepository;
            UserAccountRepository = userAccountRepository;
            PositionRepository = positionRepository;
            SyncControlRepository = syncControlRepository;
            CurrencyRepository = currencyRepository;
            CandlestickRepository = candlestickRepository;
        }
    }
}
