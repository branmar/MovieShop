using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repository
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<Purchase> GetPurchaseByUserAndMovie(int userId, int movieId);

    }
}
