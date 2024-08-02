using ImageManagementApi.Models;
using ImageManagementApi.Repository;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;

namespace ImageManagementApi.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repository;
        private readonly ILogger<ImageService> _logger;

        public ImageService(IImageRepository repository, ILogger<ImageService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Image> GetAll()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all images.");
                return new List<Image>();
            }
        }

        public Image GetById(int id)
        {
            try
            {
                return _repository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving image with ID {id}.");
                return null;
            }
        }

        public void Add(Image image)
        {
            try
            {
                _repository.Add(image);
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
                _repository.Update(id, image);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating image with ID {id}.");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting image with ID {id}.");
            }
        }
    }
}
