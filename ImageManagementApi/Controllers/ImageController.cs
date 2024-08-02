using Microsoft.AspNetCore.Mvc;
using ImageManagementApi.Models;
using ImageManagementApi.Repository;
namespace ImageManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageRepository _repository;

        public ImageController()
        {
            _repository = new ImageRepository();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Image>> GetImages()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Image> GetImage(int id)
        {
            var image = _repository.GetById(id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        [HttpPost]
        public ActionResult<Image> AddImage(Image image)
        {
            _repository.Add(image);
            return CreatedAtAction(nameof(GetImage), new { id = image.Id }, image);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateImage(int id, Image image)
        {
            if (id<0 )
            {
                return BadRequest();
            }

            _repository.Update(id, image);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}
