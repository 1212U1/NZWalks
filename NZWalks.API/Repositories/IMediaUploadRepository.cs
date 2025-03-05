using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IMediaUploadRepository
    {
        Task<Media> UploadImageAsync(Media media);
    }
}
