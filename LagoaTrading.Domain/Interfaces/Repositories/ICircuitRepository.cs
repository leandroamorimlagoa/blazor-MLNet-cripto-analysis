using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface ICircuitRepository
    {
        Task<bool> EndCircuit(long circuitId);
        Task<Circuit?> Get(long id);
        Task<IEnumerable<CircuitResponse>> GetAllFromUser(User user, CircuitRequest query);
        Task StartCircuit(Position position, CircuitType circuitType);
    }
}
