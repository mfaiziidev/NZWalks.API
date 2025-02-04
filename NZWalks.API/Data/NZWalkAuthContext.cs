using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalkAuthContext : IdentityDbContext
    {
        public NZWalkAuthContext(DbContextOptions<NZWalkAuthContext> options) : base(options)
        {
        }

        
    }
}