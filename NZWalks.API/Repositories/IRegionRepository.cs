using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> CreateAsync(Region regionDomainModel);
        Task<Region?> GetByIDAsync(Guid id);
        Task<Region?> UpdateAsync(Guid id,RegionsRequestDTO regionsRequestDTO);
        Task<Region?> DeleteAsync(Guid id);
    }
}
