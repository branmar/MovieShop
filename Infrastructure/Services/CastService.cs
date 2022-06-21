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
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public CastModel GetCastDetails(int id)
        {
            var castDetails = _castRepository.GetById(id);

            var cast = new CastModel
            {
                Id = castDetails.Id,
                Name = castDetails.Name,
                ProfilePath = castDetails.ProfilePath
            };

            foreach (var movie in castDetails.MoviesOfCast)
            {
                cast.Movies.Add(new MovieDetailsModel 
                { 
                    Id = movie.MovieId,
                    Title = movie.Movie.Title
                });
            }

            return cast;
        }
    }
}
