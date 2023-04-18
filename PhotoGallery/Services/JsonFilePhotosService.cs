using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PhotoGallery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace PhotoGallery.Services
{
    public interface IJsonFilePhotosService
    {
        IEnumerable<Photo> GetPhotos();
    }
    public class JsonFilePhotosService : IJsonFilePhotosService
    {
        public JsonFilePhotosService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "photos.json");

        public IEnumerable<Photo> GetPhotos()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<IEnumerable<Photo>>(jsonFileReader.ReadToEnd(), options);
        }


        //rating
        public void AddRating(string productId, int rating)
        {
            var products = GetPhotos();

            if (products.First(x => x.Id == productId).Ratings == null)
            {
                products.First(x => x.Id == productId).Ratings = new int[] { rating };
            }
            else
            {
                var ratings = products.First(x => x.Id == productId).Ratings.ToList();
                ratings.Add(rating);
                products.First(x => x.Id == productId).Ratings = ratings.ToArray();
            }

            using var outputStream = File.OpenWrite(JsonFileName);

            JsonSerializer.Serialize<IEnumerable<Photo>>(
                new Utf8JsonWriter(outputStream, new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true
                }),
                products
            );
        }
    }
}
