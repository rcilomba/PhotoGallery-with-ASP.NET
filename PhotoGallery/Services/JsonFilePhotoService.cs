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
        public IEnumerable<Photo> GetPhotos()
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

        public void AddRating(string photoId, int rating)
        {
            var photos = GetPhotos();

            //LINQ 
            var query = photos.First(x => x.Id == photoId);
            
            if(query.Ratings == null)
            {
                query.Ratings = new int[] {rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();  //convert them to a list
                ratings.Add(rating); // sedan lägger den till ratins
                 query.Ratings = ratings.ToArray();
            }

            using(var outputStream = File.OpenWrite(JsonFileName)) // open that file
            {
                JsonSerializer.Serialize <IEnumerable< Photo>>(
                     new Utf8JsonWriter(outputStream, new JsonWriterOptions
                     {
                         SkipValidation= true,
                         Indented=true,
                     }),
                        photos 
                );
            }

        }
    }
}