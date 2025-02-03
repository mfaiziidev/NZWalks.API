using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Repositries
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLUserRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await dbContext.Users.Include(x => x.Role).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var ExistingUser =  await dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
            if (ExistingUser == null)
            {
                return null;
            }

            //ExistingUser.Id = user.Id;
            ExistingUser.UserName = user.UserName;
            //ExistingUser.Role = user.Role;
            ExistingUser.Password = user.Password;
            ExistingUser.Email = user.Email;
            ExistingUser.RoleId = user.RoleId;

            await dbContext.SaveChangesAsync();
            return ExistingUser;
        }

        public async Task<User?> DeleteUserAsync(int id)
        {
            var ExistingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (ExistingUser == null)
            {
                return null;
            }

            dbContext.Users.Remove(ExistingUser);
            await dbContext.SaveChangesAsync();
            return ExistingUser;
        }
    }
}
