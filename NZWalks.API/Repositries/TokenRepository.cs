using Microsoft.AspNetCore.Identity;
using NZWalks.API.Repositries.IRepository;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace NZWalks.API.Repositries
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // First Create Tokens
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
