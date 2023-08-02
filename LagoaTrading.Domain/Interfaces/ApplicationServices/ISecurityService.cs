using LagoaTrading.Domain.Entities;

namespace LagoaTrading.Domain.Interfaces.ApplicationServices
{
    public interface ISecurityService
    {
        Task<User> AuthenticateUser(string emailHash, string passHash);
        Task<bool> RequestNewPassword(string passwordHash);
    }
}
