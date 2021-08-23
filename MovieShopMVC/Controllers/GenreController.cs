using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenresService _genresService;
        public GenreController (IGenresService genresService)
        {
            _genresService = genresService;
        }
        public async Task<IActionResult> Index()
        {
            var genres = await _genresService.GetAllGenres();
            return View(genres);
        }
    }
}
