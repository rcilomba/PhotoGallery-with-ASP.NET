using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Models;
using PhotoGallery.Services;

namespace PhotoGallery.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        public PhotosController(JsonFilePhotoService photoService)
        {
            this.PhotoService = photoService;
        }

        public JsonFilePhotoService PhotoService { get; }

        [HttpGet]
        public IEnumerable<Photo> Get()
        {
            return PhotoService.GetPhotos();
        }

        [Route("Rate")]
        [HttpGet]
        public ActionResult Get(
            [FromQuery]string PhotoId, 
            [FromQuery] int Rating)
        {
            PhotoService.AddRating(PhotoId, Rating);
            return Ok();
        }

    }
}
