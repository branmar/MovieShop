using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<MovieDetailsModel> GetMovieDetails(int id)
        {
            var movieDetails = await _movieRepository.GetById(id);

            var movie = new MovieDetailsModel
            {
                Id = movieDetails.Id,
                Tagline = movieDetails.Tagline,
                Title = movieDetails.Title,
                Overview = movieDetails.Overview,
                PosterUrl = movieDetails.PosterUrl,
                BackdropUrl = movieDetails.BackdropUrl,
                ImdbUrl = movieDetails.ImdbUrl,
                RunTime = movieDetails.RunTime,
                TmdbUrl = movieDetails.TmdbUrl,
                Revenue = movieDetails.Revenue,
                Budget = movieDetails.Budget,
                ReleaseDate = movieDetails.ReleaseDate,
                Price = movieDetails.Price
            };

            foreach (var genre in movieDetails.GenresOfMovie)
            {
                movie.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }

            foreach (var trailer in movieDetails.Trailers)
            {
                movie.Trailers.Add(new TrailerModel { Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl });
            }
            foreach (var castMember in movieDetails.CastOfMovie)
            {
                movie.Casts.Add(new CastModel
                {
                    Id = castMember.CastId,
                    Name = castMember.Cast.Name,
                    ProfilePath = castMember.Cast.ProfilePath,
                    Character = castMember.Character
                });
            }

            return movie;
        }

        public async Task<List<MovieCardModel>> GetTopGrossingMovies()
        {
            var movies = await _movieRepository.Get30HighestGrossingMovies();

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return movieCards;
        }

        public async Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, pageNumber);

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies.PagedData)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return new PagedResultSetModel<MovieCardModel>(pageNumber, movies.TotalRecords, pageSize, movieCards);
        }

        public async Task<List<MovieCardModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.Get30HighestRatedMovies();

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return movieCards;
        }

        public async Task<PagedResultSetModel<ReviewModel>> GetReviewsByMovie(int id, int pageSize = 30, int pageNumber = 1)
        {
            var reviews = await _movieRepository.GetReviewsByMovie(id, pageSize, pageNumber);
            var reviewList = new List<ReviewModel>();

            foreach (var review in reviews.PagedData)
            {
                reviewList.Add(new ReviewModel { MovieId = review.MovieId, Rating = review.Rating, ReviewText = review.ReviewText, UserId = review.UserId });
            }
            return new PagedResultSetModel<ReviewModel>(pageNumber, reviews.TotalRecords, pageSize, reviewList);
        }
    }
}
