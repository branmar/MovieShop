using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IUserService
    {

        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task<IEnumerable<PurchaseModel>> GetAllPurchasesForUser(int id);
        Task<PurchaseDetailsModel> GetPurchasesDetails(int userId, int movieId);
        Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<FavoriteModel> GetAllFavoritesForUser(int id);
        Task<bool> AddMovieReview(ReviewRequestModel reviewRequest);
        Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task<bool> DeleteMovieReview(int userId, int movieId);
        Task<ReviewModel> GetAllReviewsByUser(int id);

    }
}
