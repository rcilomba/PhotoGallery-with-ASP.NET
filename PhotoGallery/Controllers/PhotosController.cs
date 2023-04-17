using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Models;
using System.Text.Json;

namespace PhotoGallery.Controllers
{
    public class PhotosController : Controller
    {
        private readonly ILogger<PhotosController> _logger;
        private readonly IConfiguration _config;

        public PhotosController(ILogger<PhotosController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            var photos = GetPhotosFromJsonFile();

            return View(photos);
        }

        public IActionResult Details(string id)
        {
            var photo = GetPhotoByIdFromJsonFile(id);

            if (photo == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photo.Url.TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return new FileStreamResult(fileStream, "image/jpeg");
        }

        private List<Photo> GetPhotosFromJsonFile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "photos.json");
            var json = System.IO.File.ReadAllText(filePath);
            var photos = JsonSerializer.Deserialize<Photo>(json);
            return photos.PhotoList;
        }

        private Photo GetPhotoByIdFromJsonFile(string id)
        {
            var photos = GetPhotosFromJsonFile();
            var photo = photos.FirstOrDefault(p => p.Id == id);

            return photo;
        }
    }

}
