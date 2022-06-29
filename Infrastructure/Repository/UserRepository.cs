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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckIfMoviePurchasedByUser(int userId, int movieId)
        {
            var purchase = await _dbContext.Purchase
                                .Where(p => p.UserId == userId)
                                .FirstOrDefaultAsync(p => p.MovieId == movieId);
            return (purchase == null) ? false : true;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task<bool> CheckEmailExists(string email)
        {
            return await _dbContext.User.AnyAsync(u => u.Email == email);
        }
        public async override Task<User> GetById(int id)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}
