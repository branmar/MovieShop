using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        Task<List<MovieCardModel>> GetTopGrossingMovies();
        Task<List<MovieCardModel>> GetTopRatedMovies();

        // get movie details
        Task<MovieDetailsModel> GetMovieDetails(int id);
        Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);
        Task<PagedResultSetModel<ReviewModel>> GetReviewsByMovie(int id, int pageSize = 30, int pageNumber = 1);
    }
}
