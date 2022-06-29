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
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Purchase> GetById(int id)
        {
            var purchase = await _dbContext.Purchase
                                .FirstOrDefaultAsync(p => p.Id == id);

            return purchase;
        }
        public async Task<Purchase> GetPurchaseByUserAndMovie(int userId, int movieId)
        {
            var purchase = await _dbContext.Purchase
                                .Where(p => p.UserId == userId)
                                .FirstOrDefaultAsync(p => p.MovieId == movieId);
            return purchase;
        }
    }
}
