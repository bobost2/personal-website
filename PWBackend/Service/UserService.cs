using PWBackend.Models.Entities;
using PWBackend.Models.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PWBackend.Service
{
    public class UserService(PWDbContext context, IConfiguration configuration)
    {
        public User? GetUserByBaseLogin(string username, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username);

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user;
                }
            }

            return null;
        }

        public string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSalt"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id.ToString()),
                new Claim("userId", userInfo.Id.ToString()),
                new Claim("username", userInfo.Username)
            };

            var time = DateTime.UtcNow;
            time = time.AddDays(1);

            var token = new JwtSecurityToken("bobost.net",
                "bobost.net",
                claims,
                expires: time,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User? FindUserByClaims(ClaimsPrincipal claims)
        {
            if (claims.HasClaim(x => x.Type == "userId"))
            {
                return context.Users.Find(int.Parse(claims.FindFirst("userId")!.Value));
            }
            return null;
        }
    }
}
