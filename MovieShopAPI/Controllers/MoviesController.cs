using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopAPI.Controllers
{
    // Attribute Routing
    [Route("api/[controller]")]
    //[ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // api/movies/toprevenue
        [Route("toprevenue")]
        [HttpGet]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            // Serialization => object to another type of obejct => C# to JSON
            // C# to XML using XMLSerilizer
            // DeSerialization => JSON to C#
            // .Net Core 3.1 or less = JSON.NET => 3rd party library, icnluded
            // System.Text.JSON =>
            // 200 OK
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);

            if (movie == null)
            {
                return NotFound($"No Movie Found for that {id}");
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatingMovies()
        {
            var movies = await _movieService.GetTopRatingMovies();

            if (!movies.Any())
            {
                return NotFound("No Movie Found");
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReview(int id)
        {
            var movie = await _movieService.GetMovieReviews(id);

            if (movie == null)
            {
                return NotFound("No Movies Found");
            }

            return Ok(movie);

        }

    }
}
