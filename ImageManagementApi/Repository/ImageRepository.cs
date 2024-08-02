using System.Text.Json;
using ImageManagementApi.Models;

namespace ImageManagementApi.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly string _filePath = Path.Combine("images", "images.json");
        private List<Image> _images;
        private readonly ILogger<ImageRepository> _logger;

        public ImageRepository(ILogger<ImageRepository> logger)
        {
            _logger = logger;

            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading data from file.");
                _images = new List<Image>();
            }
        }

        public IEnumerable<Image> GetAll()
        {
            return _images;
        }

        public Image GetById(int id)
        {
            try
            {
                return _images.FirstOrDefault(i => i.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting image by ID {Id}.", id);
                return null;
            }
        }

        public void Add(Image image)
        {
            try
            {
                image.Id = _images.Any() ? _images.Max(i => i.Id) + 1 : 1;
                _images.Add(image);
                SaveToFile();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding image.");
            }
        }

        public void Update(int id, Image image)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating image with ID {Id}.", id);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var image = _images.FirstOrDefault(i => i.Id == id);
                if (image != null)
                {
                    _images.Remove(image);
                    SaveToFile();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting image with ID {Id}.", id);
            }
        }

        private void SaveToFile()
        {
            try
            {
                var json = JsonSerializer.Serialize(_images);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving data to file.");
            }
        }
    }
}
