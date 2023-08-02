using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Enumerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly LagoaTradingContext context;

        public PositionRepository(LagoaTradingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Position>> GetActivePositionsFromUser(long userId)
        => await this.context.Position.Where(p => (userId == 0 || p.UserId == userId)
                                                    && (p.State == State.Active || p.State == State.Registered))
                                        .ToListAsync();

        public Task<Position?> GetPositionById(long positionId)
        => this.context.Position.Include(p => p.Market)
                                .ThenInclude(p => p.CurrencyBase)
                                .FirstOrDefaultAsync(p => p.Id == positionId);

        public async Task<IEnumerable<Position>> GetPositionsByUser(long userId, UserPositionsRequest query)
        => await context.Position.Include(p=>p.Market)
                                 .ThenInclude(p=>p.CurrencyBase)
                                 .Where(p => p.UserId == userId
                                            && (!query.State.HasValue || p.State == query.State))
                                    .OrderByDescending(p => p.CreatedAt)
                                    .Take(query.Take)
                                    .ToListAsync();

        public async Task Save(Position position)
        {
            EntityEntry<Position> entry = null;
            if (position.Id == 0)
            {
                entry = await this.context.Position.AddAsync(position);
            }
            else
            {
                entry = this.context.Position.Update(position);
            }

            if (entry != null
                && (entry.State == EntityState.Added || entry.State == EntityState.Modified))
            {
                await this.context.SaveChangesAsync();
            }

        }
    }
}
