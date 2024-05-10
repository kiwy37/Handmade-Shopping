using Daw.DataLayer.DataBaseConenction;
using Daw.DataLayer.Models;

namespace Daw.DataLayer.Services
{
    public class ProductService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly AppDbContext _appContext;
        public ProductService(AppDbContext appContext, UnitOfWork unitOfWork)
        {
            _appContext = appContext;
            _unitOfWork = unitOfWork;
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductsRepository.GetByIdAsync(id);
        }
        public async Task<Product?> GetProductByNameAsync(string name) { return await _unitOfWork.ProductsRepository.GetProductByNameAsync(name); }
        public async Task<List<Product>> GetAllProductsAsync() { return await _unitOfWork.ProductsRepository.GetAllAsync(); }
        public async Task AddProductAsync(Product product)
        {
            await _unitOfWork.ProductsRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            await _unitOfWork.ProductsRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            await _unitOfWork.ProductsRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
