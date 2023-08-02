using LagoaTrading.Data.Repository.Contexts;
using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.Repositories;
using LagoaTrading.Shared.Enumerators;
using Microsoft.EntityFrameworkCore;

namespace LagoaTrading.Data.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LagoaTradingContext context;

        public UserRepository(LagoaTradingContext context)
        {
            this.context = context;
        }

        public async Task<User> ActivateUser(string hash)
        {
            var user = await this.context.User.IgnoreQueryFilters()
                                              .FirstOrDefaultAsync(x => x.RollingHash == hash && x.Status == UserStatus.WaitingForActivation);
            if (user == null)
            {
                return user;
            }
            user.Status = UserStatus.Active;
            user.NewRollingHash();
            await this.context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUser(string emailHash, string passHash)
        => await this.context.User.FirstOrDefaultAsync(x => x.EmailHash == emailHash && x.Password == passHash);

        public Task<User?> GetUserByEmailHash(string emailHash)
        => this.context.User.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Password == emailHash);

        public async Task<(User user, Parameter parameter)> GetUserByRollingHashWithParameter(string rollingHash)
        {
            var user = await this.context.User
                .Include(x => x.Parameter)
                .FirstOrDefaultAsync(x => x.RollingHash == rollingHash);

            return (user, user.Parameter);
        }

        public Task<User?> GetUserByRollingHash(string rollingHash)
        => this.context.User.FirstOrDefaultAsync(x => x.RollingHash == rollingHash);

        public Task Update(User user)
        {
            this.context.User.Update(user);
            return this.context.SaveChangesAsync();
        }

        public async Task UpdateCircuitCommand(long parameterId, CircuitCommand circuitCommand)
        {
            var parameter = await context.Parameter.FindAsync(parameterId);
            if (parameter == null)
            {
                return;
            }
            parameter.CircuitCommand = circuitCommand;
            context.Update(parameter);
            await context.SaveChangesAsync();
        }

        public async Task<Parameter?> GetParameterByApiKey(string apiKey, string apiSecret)
        => await this.context.Parameter.Where(p => p.ApiKey == apiKey && p.ApiSecret == apiSecret).FirstOrDefaultAsync();

        public async Task<User?> GetByEmail(string email)
        => await this.context.User.FirstOrDefaultAsync(x => x.Email == email);

        public async Task Add(User user)
        {
            await this.context.User.AddAsync(user);
            await this.context.SaveChangesAsync();
        }

        public async Task SaveParameter(Parameter parametro)
        {
            if (parametro.Id == 0)
            {
                await this.context.Parameter.AddAsync(parametro);
            }
            else
            {
                this.context.Parameter.Update(parametro);
            }
            await this.context.SaveChangesAsync();
        }

        public void UpdateParameter(Parameter parameter)
        {
            this.context.Parameter.Update(parameter);
            this.context.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetActiveUser(long userId)
        => await this.context.User.Include(u => u.Parameter)
                                    .Where(x => (userId == 0 || x.Id == userId)
                                                    && (x.Parameter.CircuitCommand == CircuitCommand.ExecutingManual
                                                            || x.Parameter.CircuitCommand == CircuitCommand.Start))
                                    .ToListAsync();

        public Task<User?> Get(long userId)
        => this.context.User.Include(u => u.Parameter).FirstOrDefaultAsync(x => x.Id == userId);

        public async Task<IEnumerable<User>> GetUsersToRunCircuit()
        => await context.User.Include(u => u.Parameter)
                             .Where(x => x.Parameter.CircuitCommand == CircuitCommand.ExecutingAutomatic
                                            || x.Parameter.CircuitCommand == CircuitCommand.Start)
                              .Select(u => u)
                              .ToListAsync();

        public Task<User?> GetUserById(long id)
        => this.context.User.Include(u => u.Parameter).FirstOrDefaultAsync(x => x.Id == id);
    }
}