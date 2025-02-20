﻿using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositries.IRepository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walks);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? SortBy = null, bool isAscending = true,
            int PageNo = 1, int PageSize = 1000);
        Task<Walk?> GetByIdAsync(int id);
        Task<Walk?> UpdateAsync(Walk walks, int id);
        Task<Walk?> DeleteByIdAsync(int id);
    }
}
