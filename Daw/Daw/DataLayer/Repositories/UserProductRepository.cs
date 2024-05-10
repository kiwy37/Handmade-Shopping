using Daw.DataLayer.DataBaseConenction;
using Daw.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Daw.DataLayer.Repositories
{
    public class UserProductRepository : BaseRepository<UserProduct>
    {

        public UserProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<List<(Product, int)>> GetAllProductsForUserByName(string name)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.Name == name);

            if (user == null)
            {
                // Handle the case where the user is not found
                return null; // or an empty list, depending on your needs
            }

            var userProducts = await _dbSet.Where(up => up.UserId == user.Id).ToListAsync();
            var productsInfo = new List<(Product, int)>();

            foreach (var userProduct in userProducts)
            {
                var product = await _appContext.Products.FirstOrDefaultAsync(p => p.Id == userProduct.ProductId);

                if (product != null)
                {
                    productsInfo.Add((product, userProduct.Quantity));
                }
            }

            return productsInfo;
        }

        public async Task<List<Product>> GetAllProductsForUser(int id)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            var userProducts = await _dbSet.Where(up => up.UserId == user.Id).ToListAsync();
            var products = new List<Product>();
            var productsId = new List<int>();
            productsId = userProducts.Select(up => up.ProductId).ToList();
            return await _appContext.Products.Where(p => productsId.Contains(p.Id)).ToListAsync();
        }
        public async Task<UserProduct?> GetUserProduct(int userId, int productId)
        {
            return await _dbSet.FirstOrDefaultAsync(up => up.UserId == userId && up.ProductId == productId);
        }
        public async Task AddUserProductByNameAndImagePath(string name, string imagePath)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.Name == name);
            var product = await _appContext.Products.FirstOrDefaultAsync(p => p.ImagePath == imagePath);
            var userProduct = new UserProduct()
            {
                UserId = user.Id,
                ProductId = product.Id,
                Quantity=1
            };
            await _dbSet.AddAsync(userProduct);
            await _appContext.SaveChangesAsync();
        }
        public async Task AddUserProduct(int userId, int productId)
        {
            var userProduct = new UserProduct()
            {
                UserId = userId,
                ProductId = productId
            };
            await _dbSet.AddAsync(userProduct);
            await _appContext.SaveChangesAsync();
        }

        public async Task DeleteOneAppereanceAsync(int userId, int productId)
        {
            var userProduct = await GetUserProduct(userId, productId);
            if (userProduct is null) return;
            _dbSet.Remove(userProduct);
            await _appContext.SaveChangesAsync();
        }
        public async Task<User> GetUserByName(string name)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.Name == name);
            return user;
        }
        public async Task<Product> GetProductByNameAsync(string name)
        {
            var product = await _appContext.Products.FirstOrDefaultAsync(p => p.ProductName == name);
            return product;
        }
        public async Task DecreaseQuantity(string name, string imagePath)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.Name == name);
            var product = await _appContext.Products.FirstOrDefaultAsync(p => p.ImagePath == imagePath);
            var userProduct = await GetUserProduct(user.Id, product.Id);
            if (userProduct is null) return;
            userProduct.Quantity--;
            if(userProduct.Quantity==0)
            {
                _dbSet.Remove(userProduct);
            }
            else
            {
                _dbSet.Update(userProduct);
            }
            await _appContext.SaveChangesAsync();
        }
        public async Task IncreaseQuantity(string name, string imagePath)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.Name == name);
            var product = await _appContext.Products.FirstOrDefaultAsync(p => p.ImagePath == imagePath);
            var userProduct = await GetUserProduct(user.Id, product.Id);
            if (userProduct is null) return;
            userProduct.Quantity++;
            _dbSet.Update(userProduct);
            await _appContext.SaveChangesAsync();
        }
        public async Task AddProductWithQuantity(string name, string imagePath, int quantity)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.Name == name);
            var product = await _appContext.Products.FirstOrDefaultAsync(p => p.ImagePath == imagePath);
            var userProduct = await GetUserProduct(user.Id, product.Id);
            if (userProduct is null)
            {
                userProduct = new UserProduct()
                {
                    UserId = user.Id,
                    ProductId = product.Id,
                    Quantity = quantity
                };
                await _dbSet.AddAsync(userProduct);
            }
            else
            {
                userProduct.Quantity += quantity;
                _dbSet.Update(userProduct);
            }
            await _appContext.SaveChangesAsync();
        }
    }
}
