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

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            Walk? existingWalk = await this.GetByIDAsync(id);
            if (existingWalk == null) { return null; }
            this.nZWalksDBContext.Walks.Remove(existingWalk);
            await this.nZWalksDBContext.SaveChangesAsync();
            return existingWalk;
            
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await this.nZWalksDBContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIDAsync(Guid id)
        {
            return await this.nZWalksDBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateByIDAsync(Guid id, Walk walk)
        {
            Walk? existingWalk = await this.GetByIDAsync(id);
            if (existingWalk == null) { return null; }
            existingWalk.Name = walk.Name;
            existingWalk.WalkImageURL = walk.WalkImageURL;
            existingWalk.Difficulty = walk.Difficulty;
            existingWalk.RegionID = walk.RegionID;
            existingWalk.LengthInKM = walk.LengthInKM;
            existingWalk.Description = walk.Description;
            await this.nZWalksDBContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
