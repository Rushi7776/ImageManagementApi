using System.Text.Json;
using ImageManagementApi.Models;

namespace ImageManagementApi.Repository
{
    public class ImageRepository
    {
        private readonly string _filePath = Path.Combine("images", "images.json");
        private List<Image> _images;

        public ImageRepository()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _images = JsonSerializer.Deserialize<List<Image>>(json) ?? new List<Image>();
            }
            else
            {
                _images = new List<Image>();
            }
        }

        public IEnumerable<Image> GetAll() => _images;

        public Image GetById(int id) => _images.FirstOrDefault(i => i.Id == id);

        public void Add(Image image)
        {
            image.Id = _images.Any() ? _images.Max(i => i.Id) + 1 : 1;
            _images.Add(image);
            SaveToFile();
        }

        public void Update(int id ,Image image)
        {
            var index = _images.FindIndex(i => i.Id == id);
            if (index != -1)
            {
               var data = _images[index] = image;
                data.DateCreated = DateTime.Now;
                data.Id = id;
                data.User = image.User;
                SaveToFile();
            }
        }

        public void Delete(int id)
        {
            var image = _images.FirstOrDefault(i => i.Id == id);
            if (image != null)
            {
                _images.Remove(image);
                SaveToFile();
            }
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_images);
            File.WriteAllText(_filePath, json);
        }
    }
}

