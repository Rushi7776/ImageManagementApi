using ImageManagementApi.Models;

namespace ImageManagementApi.Services
{
    public interface IImageService
    {
        IEnumerable<Image> GetAll();
        Image GetById(int id);
        void Add(Image image);
        void Update(int id, Image image);
        void Delete(int id);
    }
}
