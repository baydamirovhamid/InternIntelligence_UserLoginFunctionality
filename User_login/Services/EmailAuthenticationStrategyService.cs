using Microsoft.AspNetCore.Identity;
using User_login.Models;
using User_login.Repositories;

namespace User_login.Services
{
    public class EmailAuthenticationStrategyService : IAuthenticationStrategyService
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;


        public EmailAuthenticationStrategyService(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<AppUser> AuthenticateAsync(string username, string password)
        {
            var user = await _userService.GetUserByUsernameOrEmailAsync(username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return user;
        }


        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }

}
