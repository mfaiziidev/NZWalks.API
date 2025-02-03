using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositries.IRepository;

namespace NZWalks.API.Repositries
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }


        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? SortBy = null, bool isAscending = true, int PageNo = 1, int PageSize = 1000)
        {
            //return await dbContext.Walks.Include(x => x.Region).Include(x => x.Difficulty).ToListAsync();
            var walks = dbContext.Walks.Include(x => x.Region).Include(x => x.Difficulty).AsQueryable();

            //Filter Data
            if (filterOn != null && filterQuery != null)
            {
                if(filterOn == "Name")
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sort Data
            if (SortBy != null)
            {
                if (SortBy == "Name")
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (SortBy == "Length")
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination 
            var SkipResult = (PageNo - 1) * PageSize;

            return await walks.Skip(SkipResult).Take(PageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(int id)
        {
            return await dbContext.Walks.Include(x => x.Region).Include(x => x.Difficulty).FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Walk?> UpdateAsync(Walk walks, int id)
        {
            var existingWalkData = await dbContext.Walks.FirstOrDefaultAsync(x => x.id == id);

            if (existingWalkData == null)
            {
                return null;
            }

            existingWalkData.Name = walks.Name;
            existingWalkData.Description = walks.Description;
            existingWalkData.LengthInKm = walks.LengthInKm;
            existingWalkData.WalkImageURL = walks.WalkImageURL;            
            existingWalkData.RegionId = walks.RegionId;
            existingWalkData.DifficultyId = walks.DifficultyId;

            await dbContext.SaveChangesAsync();
            return existingWalkData;
        }

        public async Task<Walk?> DeleteByIdAsync(int id)
        {
            var existingWalkData = await dbContext.Walks.FirstOrDefaultAsync(x => x.id == id);

            if (existingWalkData == null)
            { return null; }

            dbContext.Walks.Remove(existingWalkData);
            await dbContext.SaveChangesAsync();

            return existingWalkData;
        }
    }   
}
