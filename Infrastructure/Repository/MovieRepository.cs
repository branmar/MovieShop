using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            // LINQ code to get top 30 grossing movies
            // select top 30 * from Movie order by Revenue

            var movies = await _dbContext.Movie.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> Get30HighestRatedMovies()
        {
            var movieRatings = await _dbContext.Review
                .Include(m => m.Movie)
                .GroupBy(m => m.MovieId)
                .OrderByDescending(o => o.Sum(m => m.Rating))
                .Select(o => new Review { MovieId = o.Key, Rating = o.Sum(m => m.Rating) })
                .Take(30).ToListAsync();
            var movie = movieRatings
                .Where(m => m.MovieId == m.Movie.Id)
                .Select(m => new Movie { Id = m.MovieId, Title = m.Movie.Title, PosterUrl = m.Movie.PosterUrl });

            return movie;
        }

        public async Task<PagedResultSetModel<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            // get total count movies for the genre
            var totalMoviesForGenre = await _dbContext.MovieGenre.Where(g => g.GenreId == genreId).CountAsync();

            var movies = await _dbContext.MovieGenre
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Movie)
                .OrderByDescending(m => m.Movie.Revenue)
                .Select(m => new Movie { Id = m.MovieId, PosterUrl = m.Movie.PosterUrl, Title = m.Movie.Title })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagedMovies = new PagedResultSetModel<Movie>(pageNumber, totalMoviesForGenre, pageSize, movies);
            return pagedMovies;

        }

        public async override Task<Movie> GetById(int id)
        {
            // select * from Movie
            // join cast and MovieCast
            // join Trailer
            // Genre and MovieGenre
            // where id = id
            var movieDetails = await _dbContext.Movie
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
                .Include(m => m.CastOfMovie).ThenInclude(m => m.Cast)
                .Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);

            return movieDetails;
        }

        public async Task<PagedResultSetModel<Review>> GetReviewsByMovie(int id, int pageSize = 30, int pageNumber = 1)
        {
            var totalReviewsForMovie = await _dbContext.Review.Where(m => m.Movie.Id == id).CountAsync();

            var reviews = await _dbContext.Review
                                .Where(r => r.Movie.Id == id)
                                .Select(r => new Review
                                {
                                    MovieId = r.MovieId,
                                    UserId = r.UserId,
                                    Rating = r.Rating,
                                    ReviewText = r.ReviewText
                                })
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize).ToListAsync();
            var pagedReviews = new PagedResultSetModel<Review>(pageNumber, totalReviewsForMovie, pageSize, reviews);
            return pagedReviews;
        }

    }
}
