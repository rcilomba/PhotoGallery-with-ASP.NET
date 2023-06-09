﻿using System;
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

        public IndexModel(ILogger<IndexModel> logger,
            JsonFilePhotoService photoService)
        {
            _logger = logger;
            PhotoService = photoService;
        }

        public JsonFilePhotoService PhotoService { get; }
        public IEnumerable<Photo>? Photo { get; private set; }

        public void OnGet() => Photo = PhotoService.GetPhotos();
    }
}

