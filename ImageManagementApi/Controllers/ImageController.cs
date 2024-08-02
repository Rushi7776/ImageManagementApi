using Microsoft.AspNetCore.Mvc;
using ImageManagementApi.Models;
using ImageManagementApi.Services;

namespace ImageManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IImageService imageService, ILogger<ImageController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Image>> GetImages()
        {
            try
            {
                var images = _imageService.GetAll();
                return Ok(images);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all images.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Image> GetImage(int id)
        {
            try
            {
                var image = _imageService.GetById(id);
                if (image == null)
                {
                    return NotFound();
                }
                return Ok(image);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching image with ID {Id}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public ActionResult<Image> AddImage(Image image)
        {
            try
            {
                _imageService.Add(image);
                return CreatedAtAction(nameof(GetImage), new { id = image.Id }, image);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding image.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateImage(int id, Image image)
        {
            try
            {
                if (id < 0)
                {
                    _logger.LogWarning("Invalid ID {Id} provided for update.", id);
                    return BadRequest();
                }

                _imageService.Update(id, image);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating image with ID {Id}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            try
            {
                _imageService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting image with ID {Id}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
