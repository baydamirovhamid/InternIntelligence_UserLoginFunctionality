using User_login.Models;

namespace User_login.Services
{
    public interface IAuthenticationStrategyService
    {
        Task<AppUser> AuthenticateAsync(string identifier, string password);
    }
}
