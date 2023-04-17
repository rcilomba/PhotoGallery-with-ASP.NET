using System.Text.Json;
using System.Text.Json.Serialization;

namespace PhotoGallery.Models
{
    public class Photos
    {
        public string? Id { get; set; }

        [JsonPropertyName("img")]
        public string? Image { get; set; }

        public string? Url { get; set; }

        public string? Title { get; set; }

        public string? Description{ get; set; }
        public int[]? Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Photos>(this);
      
    }
}
