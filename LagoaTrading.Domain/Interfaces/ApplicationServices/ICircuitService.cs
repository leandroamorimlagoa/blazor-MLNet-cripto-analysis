using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface ICircuitService
    {
        Task<Circuit?> Get(long id);
        Task<IEnumerable<CircuitResponse>> GetCircuitList(User user, CircuitRequest query);
        Task<bool> StartCircuit(User user, Parameter parameter, CircuitType circuitType);
        Task<bool> EndCircuit(long circuitId);
        Task<bool> CreatePositionToSell(long value, Position position);
        Task<IEnumerable<User>> GetUsersToRunCircuit();
    }
}
