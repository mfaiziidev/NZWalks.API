using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositries.IRepository
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<Role>> GetRoleByIdAsync(int id);
        //Task<Role?> GetRoleByIdAsync(int id);
    }
}
