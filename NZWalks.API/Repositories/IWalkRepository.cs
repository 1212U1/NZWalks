using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>>GetAllAsync(String? filterField,String? filterValue
                                    ,String? sortField,Int32 pageNumber,Int32 pageSize,Boolean isAscending);
        Task<Walk?> GetByIDAsync(Guid id);
        Task<Walk?> UpdateByIDAsync(Guid id,Walk walk);
        Task<Walk?> DeleteAsync(Guid id);

    }
}
