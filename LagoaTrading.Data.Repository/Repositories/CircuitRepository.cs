using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Extensions;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.ContractResponses;
using LagoaTrading.Shared.ContractsRequests;
using LagoaTrading.Shared.Enumerators;
using LagoaTrading.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class CircuitRepository : ICircuitRepository
    {
        private readonly LagoaTradingContext context;

        public CircuitRepository(LagoaTradingContext context)
        {
            this.context = context;
        }

        public async Task<bool> EndCircuit(long circuitId)
        {
            var circuit = await this.context.Circuit.Include(c => c.Positions)
                                                    .FirstOrDefaultAsync(c => c.Id == circuitId);
            if (circuit == null)
            {
                return false;
            }
            var parameter = await this.context.Parameter.FirstOrDefaultAsync(p => p.UserId == circuit.UserId);
            if (parameter == null)
            {
                return false;
            }


            circuit.StartValue = circuit.PositionBuy.Sum(p => (p.UnitPrice * p.Quantity));
            circuit.EndValue = circuit.PositionSell.Sum(p => (p.UnitPrice * p.Quantity));
            circuit.DifferenceValue = (circuit.EndValue - circuit.StartValue).TruncateCurrency();
            circuit.EndDateTime = DateTime.UtcNow;

            if (circuit.CircuitType == CircuitType.Single
                || parameter.CircuitCommand == CircuitCommand.Stoping)
            {
                parameter.CircuitCommand = CircuitCommand.Stopped;
            }

            parameter.AvaliableValue += circuit.EndValue;

            context.Parameter.Update(parameter);
            context.Circuit.Update(circuit);
            context.SaveChanges();

            return true;
        }

        public async Task<Circuit?> Get(long id)
        => await this.context.Circuit.Include(c => c.Positions)
                                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<CircuitResponse>> GetAllFromUser(User user, CircuitRequest query)
        {
            var list = await this.context.Circuit.Include(c => c.Positions)
                                            .ThenInclude(c => c.Market.CurrencyBase)
                                           .Where(c => c.UserId == user.Id
                                                            && (!query.FromDate.HasValue || c.StartDateTime >= query.FromDate.Value))
                                                    .OrderByDescending(c => c.StartDateTime)
                                                    .Take(query.Take)
                                                    .ToListAsync();

            var result = new List<CircuitResponse>();
            foreach (var c in list)
            {
                var response = new CircuitResponse();
                response.PositionBuy = c.PositionBuy.ToUserPosition();
                response.PositionSell = c.PositionSell.ToUserPosition();
                response.Profit = c.DifferenceValue;
                response.DurationHours = !c.EndDateTime.HasValue
                                        ? -1
                                        : (decimal)(c.EndDateTime.Value - c.StartDateTime).TotalHours;

                result.Add(response);
            }
            return result;
        }

        public async Task StartCircuit(Position position, CircuitType circuitType)
        {
            var circuit = new Circuit
            {
                UserId = position.UserId,
                CircuitType = circuitType,
                StartDateTime = DateTime.UtcNow,
                StartValue = 0,
                EndValue = 0,
                DifferenceValue = 0
            };
            await this.context.Circuit.AddAsync(circuit);
            await this.context.SaveChangesAsync();

            position.CircuitId = circuit.Id;
            await this.context.Position.AddAsync(position);
            await this.context.SaveChangesAsync();
        }
    }
}
