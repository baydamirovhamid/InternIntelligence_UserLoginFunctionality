using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User_login.Models;

namespace User_login.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;

            _secretKey = _configuration["JwtSettings:SecretKey"];
            _issuer = _configuration["JwtSettings:Issuer"];
            _audience = _configuration["JwtSettings:Audience"];
        }

        public string GenerateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: expiration,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
