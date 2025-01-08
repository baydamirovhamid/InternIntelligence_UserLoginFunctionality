using User_login.Models;

namespace User_login.Services
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
