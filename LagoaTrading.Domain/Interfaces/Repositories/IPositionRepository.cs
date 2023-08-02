using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.ContractsRequests;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetActivePositionsFromUser(long userId);
        Task<Position?> GetPositionById(long positionId);
        Task<IEnumerable<Position>> GetPositionsByUser(long userId, UserPositionsRequest query);
        Task Save(Position position);
    }
}
