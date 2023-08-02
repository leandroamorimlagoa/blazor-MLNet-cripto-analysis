using LagoaTrading.Domain.Entities;
using LagoaTrading.Shared.Enumerators;

namespace LagoaTrading.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> Get(long userId);
        Task<IEnumerable<User>> GetActiveUser(long userId);
        Task<User> ActivateUser(string hash);
        Task<User?> GetUser(string emailHash, string passHash);
        Task<User?> GetUserByEmailHash(string emailHash);
        Task<(User user, Parameter parameter)> GetUserByRollingHashWithParameter(string rollingHash);
        Task<User?> GetUserByRollingHash(string rollingHash);
        Task Update(User user);
        Task UpdateCircuitCommand(long parameterId, CircuitCommand circuitCommand);
        Task<Parameter?> GetParameterByApiKey(string apiKey, string apiSecret);
        Task<User?> GetByEmail(string email);
        Task Add(User user);
        Task SaveParameter(Parameter parametro);
        void UpdateParameter(Parameter parameter);
        Task<IEnumerable<User>> GetUsersToRunCircuit();
        Task<User?> GetUserById(long id);
    }
}
