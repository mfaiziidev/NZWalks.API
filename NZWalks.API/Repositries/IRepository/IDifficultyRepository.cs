using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositries.IRepository
{
    public interface IDifficultyRepository
    {
        Task<Difficulty> CreateAsync(Difficulty difficulty);
        Task<List<Difficulty>> GetAllAsync();
        Task<Difficulty?> GetDifficultyByIdAsync(Guid id);
        Task<Difficulty?> UpdateAsync(Difficulty difficulty, Guid id);
        Task<Difficulty?> DeleteAsync(Guid id);
    }
}
