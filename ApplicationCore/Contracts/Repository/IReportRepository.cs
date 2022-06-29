using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repository
{
    public interface IReportRepository
    {
        Task<IEnumerable<MoviesReportModel>> GetTopPurchasedMovies(DateTime? fromDate = null, DateTime? toDate = null, int pageSize = 30, int pageIndex = 1);
    }
}
