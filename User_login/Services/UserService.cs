using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using User_login.Contexts;
using User_login.DTOs;
using User_login.Models;
using User_login.Repositories;

namespace User_login.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public UserService(IUserRepository userRepository, AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userRepository = userRepository;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<AppUser> GetUserByUsernameOrEmailAsync(string identifier)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == identifier || u.Email == identifier);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }

}
