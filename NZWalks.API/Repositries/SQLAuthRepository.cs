using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Repositries
{
    public class SQLAuthRepository : IAuthRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLAuthRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //public async Task<Role?> GetRoleByIdAsync(int id)
        //{
        //    //return await dbContext.Roles.ToListAsync();
        //    return await dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);

        //}

        public async Task<List<Role>> GetRoleByIdAsync(int roleId)
        {
            return await dbContext.Roles.Where(x => x.Id == roleId).ToListAsync();
        }


        public async Task<User?> GetUserByUsernameAsync(string loginIdentifier)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == loginIdentifier || x.Email == loginIdentifier);
        }
    }
}
