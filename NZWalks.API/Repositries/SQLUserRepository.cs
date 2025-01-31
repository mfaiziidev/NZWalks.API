using NZWalks.API.Models.Domain;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Repositries
{
    public class SQLUserRepository : IUserRepository
    {
        public Task<User> CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
