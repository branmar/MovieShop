using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IReportRepository _reportRepository;

        public AdminService(IMovieRepository movieRepository, IReportRepository reportRepository)
        {
            _movieRepository = movieRepository;
            _reportRepository = reportRepository;
        }

        public async Task<bool> AddMovie(Movie movie)
        {
            var result = await _movieRepository.Add(movie);
            if (result == null) return false;
            return true;
        }

        public async Task<List<MovieCardModel>> GetTopPurchasedMovies(DateTime? fromDate = null, DateTime? toDate = null, int pageSize = 30, int pageIndex = 1)
        {
            var result = await _reportRepository.GetTopPurchasedMovies(fromDate, toDate, pageSize, pageIndex);
            return (List<MovieCardModel>)result;
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            var result = await _movieRepository.Update(movie);
            if (result == null) return false;
            return true;
        }
    }
}
