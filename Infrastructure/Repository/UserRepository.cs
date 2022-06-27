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
    }
}
