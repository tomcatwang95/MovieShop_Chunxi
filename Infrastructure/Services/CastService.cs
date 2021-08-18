using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;


namespace Infrastructure.Services
{
    public class CastService :ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastResponseModel> GetCastDetails(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var castModel = new CastResponseModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                ProfilePath = cast.ProfilePath,
                TmdbUrl=cast.TmdbUrl
            };

            castModel.Movies = new List<MovieDetailsResponseModel>();

            foreach (var movie in cast.MovieCasts)
            {
                castModel.Movies.Add(new MovieDetailsResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl,
                    ReleaseDate=movie.Movie.ReleaseDate

                });
            }

            return castModel;

        }
    }
}
