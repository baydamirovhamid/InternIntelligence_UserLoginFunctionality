using Microsoft.EntityFrameworkCore;
using User_login.Models;
using User_login.Services;

namespace User_login.Contexts
{
    public class AuthenticationContext: DbContext
    {
        private IAuthenticationStrategyService _strategy;

        public void SetStrategy(IAuthenticationStrategyService strategy)
        {
            _strategy = strategy;
        }

        public async Task<AppUser> AuthenticateAsync(string identifier, string password)
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Authentication strategy is not set");
            }

            return await _strategy.AuthenticateAsync(identifier, password);
        }
    }

}
