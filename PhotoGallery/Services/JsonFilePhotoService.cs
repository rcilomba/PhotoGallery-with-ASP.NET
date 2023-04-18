using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PhotoGallery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoGallery.Services;

namespace PhotoGallery.Services
{
    public class JsonFilePhotoService

    {
        public JsonFilePhotoService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnviroment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnviroment { get; }
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnviroment.WebRootPath, "data", "photos.json"); }
        }
        public IEnumerable<Photo> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Photo[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
    }
}
