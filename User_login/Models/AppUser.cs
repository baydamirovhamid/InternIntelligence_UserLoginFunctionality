using Microsoft.AspNetCore.Identity;

namespace User_login.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; }
    }
}
