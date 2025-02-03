using NZWalks.API.Models.Domain;  // Your custom User model
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

        // Modify method to accept your custom User model
        public string CreateJWTToken(User user, List<string> roles)
        {
            // Create the list of claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim(ClaimTypes.Name, user.Username),  // Assuming Username field in your domain model
                //new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())  // Custom User ID as claim
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Define key for signing the token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create JWT token
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}












//using Microsoft.AspNetCore.Identity;
//using NZWalks.API.Repositries.IRepository;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;

//namespace NZWalks.API.Repositries
//{
//    public class TokenRepository : ITokenRepository
//    {
//        private readonly IConfiguration configuration;

//        public TokenRepository(IConfiguration configuration)
//        {
//            this.configuration = configuration;
//        }
//        public string CreateJWTToken(IdentityUser user, List<string> roles)
//        {
//            // First Create Tokens
//            var claims = new List<Claim>();

//            claims.Add(new Claim(ClaimTypes.Email, user.Email));

//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }

//            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
//            var credential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

//            var Token = new JwtSecurityToken(
//                configuration["Jwt:Issuer"],
//                configuration["Jwt:Audience"],
//                claims,
//                expires: DateTime.Now.AddHours(1),
//                signingCredentials: credential
//            );

//            return new JwtSecurityTokenHandler().WriteToken(Token);
//        }
//    }
//}
