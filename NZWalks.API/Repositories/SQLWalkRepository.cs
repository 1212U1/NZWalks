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

        public async Task<List<Walk>> GetAllAsync(String? filterField, String? filterValue
                                                  ,String? sortField, Int32 pageNumber, Int32 pageSize, Boolean isAscending=true)
        {
           IQueryable<Walk> walks = this.nZWalksDBContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            if (filterField!=null && filterValue != null && filterField.Equals("Name"))
            {
                walks = walks.Where(t=>t.Name.Contains(filterValue));
            }
            if(!String.IsNullOrEmpty(sortField))
            {
                walks = isAscending?walks.OrderBy(x=>x.Name):walks.OrderByDescending(x=>x.Name);
            }
            walks = walks.Skip((pageNumber - 1) * pageSize).Take(pageSize);
           return await walks.ToListAsync();
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
