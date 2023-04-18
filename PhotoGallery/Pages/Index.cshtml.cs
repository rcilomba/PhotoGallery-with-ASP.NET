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
        private readonly JsonFilePhotosService photoService;

        public IndexModel(JsonFilePhotosService photoService)
        {
            this.photoService = photoService;
        }


        public IEnumerable<Photo> Photos { get; private set; }

        public void OnGet()
        {
            Photos = photoService.GetPhotos();
        }

    }
}
