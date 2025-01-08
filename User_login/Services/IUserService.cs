using Microsoft.AspNetCore.Identity;
using User_login.DTOs;
using User_login.Models;

namespace User_login.Services
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<AppUser> GetUserByUsernameOrEmailAsync(string username);
        Task SignOutAsync();
    }
}
