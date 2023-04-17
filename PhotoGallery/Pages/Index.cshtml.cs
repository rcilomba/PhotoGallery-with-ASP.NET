using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoGallery.Services;
using System.Collections.Generic;
using PhotoGallery.Models;
using Microsoft.Extensions.Logging;



namespace PhotoGallery.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public JsonFilePhotoService PhotoService;
    public IEnumerable<Photos> Photos { get; set; }

    public IndexModel(
        ILogger<IndexModel> logger,
        JsonFilePhotoService photoService) 
    {
        _logger = logger;
        PhotoService = photoService;
    }

    public void OnGet()
    {
        Photos = PhotoService.GetProducts();
    }
}