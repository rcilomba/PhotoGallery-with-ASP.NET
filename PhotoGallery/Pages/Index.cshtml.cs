using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoGallery.Services;
using System.Collections.Generic;
using PhotoGallery.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace PhotoGallery.Pages
{
    public class IndexModel : PageModel
    {
        private readonly JsonFilePhotosService _photoService;

        public IndexModel(JsonFilePhotosService photoService)
        {
            PhotoService = photoService;
        }


        public IEnumerable<Photo> Photos { get; private set; }

        public void OnGet()
        {
            Photos = PhotoService.GetPhotos();
        }

    }
}
