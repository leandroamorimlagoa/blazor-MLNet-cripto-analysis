using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.ContractsRequests;

namespace LagoaTrading.Application.Service.Implementations
{
    public class PositionService : IPositionService
    {
        private readonly IApplicationRepositories applicationRepositories;

        public PositionService(IApplicationRepositories applicationRepositories)
        {
            this.applicationRepositories = applicationRepositories;
        }

        public Task<IEnumerable<Position>> GetActivePositionsFromUser(long userId)
        => this.applicationRepositories.PositionRepository.GetActivePositionsFromUser(userId);

        public Task<Position?> GetPositionById(long positionId)
        => this.applicationRepositories.PositionRepository.GetPositionById(positionId);

        public Task<IEnumerable<Position>> GetPositionsByUser(long id, UserPositionsRequest query)
        => this.applicationRepositories.PositionRepository.GetPositionsByUser(id, query);

        public Task Save(Position position)
        => this.applicationRepositories.PositionRepository.Save(position);
    }
}
