using NZwalks.api.Models.Domain;

namespace NZwalks.api.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
