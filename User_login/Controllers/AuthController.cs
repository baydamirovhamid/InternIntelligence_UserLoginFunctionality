using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User_login.Contexts;
using User_login.DTOs;
using User_login.Models;
using User_login.Repositories;
using User_login.Services;

namespace User_login.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationContext _authenticationContext;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IAuthenticationStrategyService _authenticationStrategyService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService; 

        public AuthController(
            AuthenticationContext authenticationContext,
            IUserRepository userRepository,
            IUserService userService,
            IAuthenticationStrategyService authenticationStrategyService,
            UserManager<AppUser> userManager,
            ITokenService tokenService) 
        {
            _authenticationContext = authenticationContext;
            _userRepository = userRepository;
            _userService = userService;
            _authenticationStrategyService = authenticationStrategyService;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                IAuthenticationStrategyService strategy;

                // Determine which authentication strategy to use (email or username)
                if (request.Identifier.Contains("@"))
                {
                    strategy = new EmailAuthenticationStrategyService(_userService, _userManager);
                }
                else
                {
                    strategy = new UsernameAuthenticationStrategyService(_userService, _userManager);
                }

                // Set the authentication strategy
                _authenticationContext.SetStrategy(strategy);

                // Authenticate the user
                var user = await _authenticationContext.AuthenticateAsync(request.Identifier, request.Password);

                // Generate the token using the injected TokenService
                var token = _tokenService.GenerateToken(user);

                // Return the token with a success message
                return Ok(new AuthResponseDto
                {
                    Token = token,
                    Message = "Login successful"
                });
            }
            catch (UnauthorizedAccessException)
            {
                // Return unauthorized error if credentials are invalid
                return Unauthorized(new { error = "Invalid credentials" });
            }
            catch (Exception ex)
            {
                // Return internal server error if any other exception occurs
                return StatusCode(500, new { error = ex.Message });
            }
        }
    

    [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            var user = new AppUser
            {
                UserName = signUpDto.UserName,
                Email = signUpDto.Email
            };

            var result = await _userService.CreateUserAsync(user, signUpDto.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User created successfully!" });
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { message = "User creation failed.", errors });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();
            return Ok();
           
        }
    }
}



