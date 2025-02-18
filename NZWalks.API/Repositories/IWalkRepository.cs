﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>>GetAllAsync();
        Task<Walk?> GetByIDAsync(Guid id);
        Task<Walk?> UpdateByIDAsync(Guid id,Walk walk);
        Task<Walk?> DeleteAsync(Guid id);

    }
}
