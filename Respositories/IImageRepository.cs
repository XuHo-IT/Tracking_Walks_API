

using NZWalk.API.Model.Domain;

namespace NZWalk.API.Respositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
