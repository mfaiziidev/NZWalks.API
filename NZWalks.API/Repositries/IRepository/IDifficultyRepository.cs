using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositries.IRepository
{
    public interface IDifficultyRepository
    {
        Task<Difficulty> CreateAsync(Difficulty difficulty);
        Task<List<Difficulty>> GetAllAsync();
        Task<Difficulty?> GetDifficultyByIdAsync(int id);
        Task<Difficulty?> UpdateAsync(Difficulty difficulty, int id);
        Task<Difficulty?> DeleteAsync(int id);
    }
}
