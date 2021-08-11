using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;
using Infrastructure.Services;
using System.Linq;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            //get top revenue movie and display on the view
            var movies = _movieService.GetTopRevenueMovies();
            //3 ways to pass data from controller to view
            //1.Strong typed models
            //2.View Bag
            //3.View Data
            //C#

            ViewBag.PageTitle = "Top Revenue Movies";
            ViewData["TotalMovies"] = movies.Count();

            return View(movies);
        }

        //Clean Architecture

        //Interfaces

        //IMovieService
        //class MovieService: IMovieSerivce, IGenreService
        //{
        //}

        //Dependency Injection
        //Application core layer
        //Entities => C# classes that represent your domain/databse object =>
        //Models => 

        // 15 tables
        //20 columns in the movie table
        //Entity => Movie 20 properties

        //Models => UI
        //Movie list => MovieCardModel => id, title, posterimage
        //DTO (Data Transfer Objects) => API

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
