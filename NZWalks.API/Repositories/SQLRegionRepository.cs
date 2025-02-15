using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;
        public SQLRegionRepository(NZWalksDBContext dBContext)
        {
            nZWalksDBContext = dBContext;
        }

        public async Task<Region> CreateAsync(Region regionDomainModel)
        {
            await nZWalksDBContext.Regions.AddAsync(regionDomainModel);
            await nZWalksDBContext.SaveChangesAsync();
            return regionDomainModel;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await nZWalksDBContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIDAsync(Guid id)
        {
            return await nZWalksDBContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id,RegionsRequestDTO regionDomainModel)
        {
            Region? region = await this.GetByIDAsync(id);
            if (region == null) { return null; }
            region.RegionImageURL = regionDomainModel.RegionImageURL;
            region.Name = regionDomainModel.Name;
            region.Code = regionDomainModel.Code;
            await nZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            Region? region = await this.GetByIDAsync(id);
            if (region == null) { return null; }
            this.nZWalksDBContext.Regions.Remove(region);
            await this.nZWalksDBContext.SaveChangesAsync();
            return region;

        }
    }
}