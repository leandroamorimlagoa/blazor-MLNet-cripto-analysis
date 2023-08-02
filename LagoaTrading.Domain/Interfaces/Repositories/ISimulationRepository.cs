using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.Objects;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface ISimulationRepository
    {
        Task<SimulationPosition> SimulateBuy(Parameter parameter, long marketId);
        SimulationPosition SimulateSell(Parameter parameter, decimal baseUnitPrice, decimal TotalCryptoToSell);
    }
}
