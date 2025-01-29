using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Repositries
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLDifficultyRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            dbContext.Difficulties.Add(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }
                
        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await dbContext.Difficulties.ToListAsync();
        }

        public async Task<Difficulty?> GetDifficultyByIdAsync(Guid id)
        {
            return await dbContext.Difficulties.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Difficulty?> UpdateAsync(Difficulty difficulty, Guid id)
        {
            var ExistingDifficulty = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.id == id);
            if (ExistingDifficulty == null)
            {
                return null;
            }

            ExistingDifficulty.Name = difficulty.Name;
            await dbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty?> DeleteAsync(Guid id)
        {
            var ExistingDifficulty = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.id == id);
            if (ExistingDifficulty == null)
            {
                return null;
            }
            dbContext.Remove(ExistingDifficulty);
            await dbContext.SaveChangesAsync();
            return ExistingDifficulty;
        }

    }
}
