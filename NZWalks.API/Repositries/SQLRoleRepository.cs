using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Repositries
{
    public class SQLRoleRepository : IRoleRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRoleRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Role> CreateAsync(Role role)
        {
            dbContext.Roles.Add(role);
            await dbContext.SaveChangesAsync();
            return role;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await dbContext.Roles.ToListAsync();
        }
    }
}
