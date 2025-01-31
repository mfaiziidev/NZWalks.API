using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositries.IRepository
{
    public interface IRoleRepository
    {
        Task<Role> CreateAsync(Role role);
        Task<List<Role>> GetAllAsync();
    }
}
