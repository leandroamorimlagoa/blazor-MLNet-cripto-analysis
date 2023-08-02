using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.ContractsRequests;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetActivePositionsFromUser(long userId);
        Task<Position?> GetPositionById(long positionId);
        Task<IEnumerable<Position>> GetPositionsByUser(long id, UserPositionsRequest query);
        Task Save(Position position);
    }
}
