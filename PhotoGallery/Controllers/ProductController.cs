using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Models;
using PhotoGallery.Pages;
using System.Text.Json;
using PhotoGallery.Services;


namespace PhotoGallery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        public ProductsController(Pages.JsonFilePhotoService productService) =>
            ProductService = productService;

        public Pages.JsonFilePhotoService ProductService { get; }

        [HttpGet]
        public IEnumerable<Photo> Get() => ProductService.GetProducts();

        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            if (request?.ProductId == null)
                return BadRequest();

            ProductService.AddRating(request.ProductId, request.Rating);

            return Ok();
        }

        public class RatingRequest
        {
            public string? ProductId { get; set; }
            public int Rating { get; set; }
        }
    }

}
