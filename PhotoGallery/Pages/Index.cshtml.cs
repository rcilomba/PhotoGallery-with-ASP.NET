using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoGallery.Models;
using PhotoGallery.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace PhotoGallery.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFilePhotoService ProductService;
        public IEnumerable<Photo> Products { get; set; }



        public IndexModel(
            ILogger<IndexModel> logger,
            JsonFilePhotoService productService)
        {
            _logger = logger;
            ProductService = productService;
        }


        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}

