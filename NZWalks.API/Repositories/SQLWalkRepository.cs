using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository:IWalkRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public SQLWalkRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await this.nZWalksDBContext.AddAsync(walk);
            await this.nZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await this.nZWalksDBContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
    }
}
