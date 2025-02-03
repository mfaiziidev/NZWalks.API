using Microsoft.AspNetCore.Identity;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositries.IRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> roles);
    }
}
