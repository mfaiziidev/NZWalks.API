using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositries.IRepository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walks);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? SortBy = null, bool isAscending = true);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Walk walks, Guid id);
        Task<Walk?> DeleteByIdAsync(Guid id);
    }
}
