using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface IUserService
    {
        Task<User?> GetUserById(long id);
        Task<User> ActivateUser(string hash);
        Task<User?> GetByEmail(string email);
        Task<Parameter?> GetParameterByApiKey(string apiKey, string apiSecret);
        Task<User?> GetUserByRollingHash(string rollingHash);
        Task<(User user, Parameter parameter)> GetUserByRollingHashWithParameter(string rollingHash);
        Task Update(User user);
        Task Add(User user);
        Task SaveParameter(Parameter parametro);
        void UpdateParameter(Parameter parameter);
    }
}
