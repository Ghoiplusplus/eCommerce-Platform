using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Models;

namespace UserService.Services
{
    public class JwtService : IJwtService
    {
        private static string _secret = Environment.GetEnvironmentVariable("SECRET");
        public string GenerateJwtToken(UserModel user)
        {
            var claims = new List<Claim> {
                new Claim("UserId", user.UserId.ToString())
            };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: "userservice",
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
