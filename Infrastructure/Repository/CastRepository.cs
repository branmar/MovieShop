using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CastRepository : Repository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override Cast GetById(int id)
        {
            var cast = _dbContext.Cast
                .Include(m => m.MoviesOfCast).ThenInclude(m => m.Movie)
                .FirstOrDefault(m => m.Id == id);
            return cast;
        }
    }
}
