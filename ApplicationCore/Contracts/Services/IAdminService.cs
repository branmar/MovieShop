using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IAdminService
    {
        public Task<bool> AddMovie(Movie movie);
        public Task<bool> UpdateMovie(Movie movie);
        public Task<List<MovieCardModel>> GetTopPurchasedMovies(DateTime? fromDate = null, DateTime? toDate = null, int pageSize = 30, int pageIndex = 1);
    }
}
