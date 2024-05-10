using Daw.DataLayer.Models;
using Daw.DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Daw.Controllers
{
    public class UserProductsController : Controller
    {
        private readonly UserProductService _userProductService;
        public UserProductsController(UserProductService userProductService)
        {
            _userProductService = userProductService;
        }

        [HttpGet("GetAllProductsForUser", Name = "GetAllProductsForUser")]

        public async Task<IActionResult> GetAllProductsForUser(int userId)
        {
            var products = await _userProductService.GetAllProductsForUser(userId);
            return Ok(products);
        } 
        [HttpGet("UsersProducts", Name = "UsersProducts")]

        public async Task<IActionResult> UsersProducts(string Name)
        {
           var products = await _userProductService.GetAllProductsForUserByName(Name);
            var productInfos = products.Select(p => new DataLayer.Models.ProductInfo
            {
                Produs = p.Item1,
                Quantity = p.Item2
            }).ToList();
            return Ok(productInfos);
        }
        [HttpPost("AddProductToUser", Name = "AddProductToUser")]
        public async Task<IActionResult> AddProductToUser(string username, string productName)
        {
            
            var User = await _userProductService.GetUserByName(username);
            var Product = await _userProductService.GetProductByNameAsync(productName);
            var UserProduct = new UserProduct
            {
                UserId = User.Id,
                ProductId = Product.Id
            };
            _userProductService.AddUserProductAsync(UserProduct);
            return (Ok());
        }
        [HttpPost("AddProductToUserByImagePath", Name = "AddProductToUserByImagePath")]
        public async Task<IActionResult> AddProductToUserByImagePath([FromQuery] string username, [FromQuery] string imagePath)
        {
            await _userProductService.AddUserProductByNameAndImagePath(username, imagePath);
            return Ok();
        }
        [HttpPost("DecreaseQuantity", Name = "DecreaseQuantity")]
        public async Task<IActionResult> DecreaseQuantity([FromQuery] string name, [FromQuery] string imagePath)
        {
            await _userProductService.DecreaseQuantity(name, imagePath);
            return Ok();
        }

        [HttpPost("IncreaseQuantity", Name = "IncreaseQuantity")]
        public async Task<IActionResult> IncreaseQuantity([FromQuery] string name, [FromQuery] string imagePath)
        {
            await _userProductService.IncreaseQuantity(name, imagePath);
            return Ok();
        }
        [HttpPost("AddProductWithQuantity", Name = "AddProductWithQuantity")]
        public async Task<IActionResult> AddProductWithQuantity([FromQuery] string name, [FromQuery] string imagePath, [FromQuery] int quantity)
        {
            await _userProductService.AddProductWithQuantity(name, imagePath, quantity);
            return Ok();
        }
    }
}
