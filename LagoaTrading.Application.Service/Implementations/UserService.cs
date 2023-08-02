using LagoaTrading.Domain.Entities;
using LagoaTrading.Domain.Interfaces.ApplicationServices;
using LagoaTrading.Domain.Interfaces.Repositories;

namespace LagoaTrading.Application.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IApplicationRepositories applicationRepositories;

        public UserService(IApplicationRepositories applicationRepositories)
        {
            this.applicationRepositories = applicationRepositories;
        }

        public Task<User> ActivateUser(string hash)
        => this.applicationRepositories.UserRepository.ActivateUser(hash);

        public Task Add(User user)
        => this.applicationRepositories.UserRepository.Add(user);

        public Task SaveParameter(Parameter parametro)
        => this.applicationRepositories.UserRepository.SaveParameter(parametro);

        public Task<User?> GetByEmail(string email)
        => this.applicationRepositories.UserRepository.GetByEmail(email);

        public Task<Parameter?> GetParameterByApiKey(string apiKey, string apiSecret)
        => this.applicationRepositories.UserRepository.GetParameterByApiKey(apiKey, apiSecret);

        public Task<User?> GetUserById(long id)
        => this.applicationRepositories.UserRepository.GetUserById(id);

        public Task<User?> GetUserByRollingHash(string rollingHash)
        => this.applicationRepositories.UserRepository.GetUserByRollingHash(rollingHash);

        public Task<(User user, Parameter parameter)> GetUserByRollingHashWithParameter(string rollingHash)
        => this.applicationRepositories.UserRepository.GetUserByRollingHashWithParameter(rollingHash);

        public Task Update(User user)
        => this.applicationRepositories.UserRepository.Update(user);

        public void UpdateParameter(Parameter parameter)
        => this.applicationRepositories.UserRepository.UpdateParameter(parameter);
    }
}
