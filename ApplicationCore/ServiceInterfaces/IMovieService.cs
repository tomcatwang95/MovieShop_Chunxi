using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();

        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<MovieCardResponseModel> CreateMovie(MovieCreateRequestModel movie);
        Task<MovieDetailsResponseModel> UpdateMovie(MovieUpdateRequestModel movie);
        Task<List<MovieCardResponseModel>> GetfilterGenres(int id);
        Task<List<MovieCardResponseModel>> GetTopRatingMovies();
        Task<List<MovieReviewsModel>> GetMovieReviews(int id);
    }
}
