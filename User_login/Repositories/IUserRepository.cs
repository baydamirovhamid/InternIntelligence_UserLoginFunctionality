using Microsoft.AspNetCore.Identity;
using User_login.Models;

namespace User_login.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser> FindByNameAsync(string userName);
        Task<AppUser> FindByEmailAsync(string email);
        Task SaveChangesAsync();
    }
}
