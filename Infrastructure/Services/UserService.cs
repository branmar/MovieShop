using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
        }

        public Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FavoriteExists(int id, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<FavoriteModel> GetAllFavoritesForUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PurchaseModel>> GetAllPurchasesForUser(int id)
        {
            var purchases = await _purchaseRepository.GetAll();
            var purchasesModel = purchases.Select(p => new PurchaseModel
            {
                Id = p.Id,
                UserId = p.UserId,
                MovieId = p.MovieId
            });
            return purchasesModel;
        }

        public Task<ReviewModel> GetAllReviewsByUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PurchaseDetailsModel> GetPurchasesDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            if (await _userRepository.CheckIfMoviePurchasedByUser(userId, purchaseRequest.MovieId))
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            if (await IsMoviePurchased(purchaseRequest, userId))
            {
                throw new ConflictException("You've already purchased this movie.");
            }
            var newPurchase = new Purchase
            {
                UserId = purchaseRequest.UserId,
                MovieId = purchaseRequest.MovieId,
                TotalPrice = purchaseRequest.TotalPrice,
                PurchaseDateTime = DateTime.Today,
                PurchaseNumber = purchaseRequest.PurchaseNumber
            };

            var savedPurchase = await _purchaseRepository.Add(newPurchase);
            if (savedPurchase.Id > 0)
            {
                return true;
            }
            return false;
        }

        public Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }
    }
}
