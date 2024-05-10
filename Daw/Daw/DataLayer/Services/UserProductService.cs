using Daw.DataLayer.DataBaseConenction;
using Daw.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Daw.DataLayer.Services
{
    public class UserProductService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly AppDbContext _appContext;
        public UserProductService(AppDbContext appContext, UnitOfWork unitOfWork)
        {
            _appContext = appContext;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserProduct?> GetUserProductByIdAsync(int id)
        {
            return await _unitOfWork.UserProductRepository.GetByIdAsync(id);
        }
        public async Task<List<Product>> GetAllProductsForUser(int userId)
        {
            return await _unitOfWork.UserProductRepository.GetAllProductsForUser(userId);
        }
        public async Task<List<(Product,int)>> GetAllProductsForUserByName(string userName)
        {
            return await _unitOfWork.UserProductRepository.GetAllProductsForUserByName(userName);
        }
        public async Task DeleteAsync(int userId, int productId)
        {
            await _unitOfWork.UserProductRepository.DeleteOneAppereanceAsync(userId, productId);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<User> GetUserByName(string name)
        {
            return await _unitOfWork.UserProductRepository.GetUserByName(name);
           
        }
        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _unitOfWork.UserProductRepository.GetProductByNameAsync(name);
        }
        public async Task AddUserProductAsync(UserProduct userProduct)
        {
            await _unitOfWork.UserProductRepository.AddAsync(userProduct);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task AddUserProductByNameAndImagePath(string name, string imagePath)
        {
            await _unitOfWork.UserProductRepository.AddUserProductByNameAndImagePath(name, imagePath);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DecreaseQuantity(string name, string imagePath)
        {
            await _unitOfWork.UserProductRepository.DecreaseQuantity(name, imagePath);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task IncreaseQuantity(string name, string imagePath)
        {
            await _unitOfWork.UserProductRepository.IncreaseQuantity(name, imagePath);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task AddProductWithQuantity(string name, string imagePath, int quantity)
        {
            await _unitOfWork.UserProductRepository.AddProductWithQuantity(name, imagePath, quantity);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
