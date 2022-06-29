using ApplicationCore.Contracts.Repository;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ReportRepository : IReportRepository
    {
        public Task<IEnumerable<MoviesReportModel>> GetTopPurchasedMovies(DateTime? fromDate = null, DateTime? toDate = null, int pageSize = 30, int pageIndex = 1)
        {
            throw new NotImplementedException();
        }
    }
}
