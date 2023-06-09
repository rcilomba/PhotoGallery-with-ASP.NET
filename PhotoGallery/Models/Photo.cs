using System.Text.Json;
using System.Text.Json.Serialization;

namespace PhotoGallery.Models
{
    public class Photo
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Id { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? Maker { get; set; }

        [JsonPropertyName("img")]
        public string? Image { get; set; }
        public string? Url { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Photo>(this);
    }
}