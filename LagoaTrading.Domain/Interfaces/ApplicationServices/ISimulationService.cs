using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.ContractResponses;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface ISimulationService
    {
        Task<SimulationResponse> GetSimulation(Parameter parameter, Market market);
    }
}
