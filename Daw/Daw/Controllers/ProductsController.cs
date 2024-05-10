using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Daw.DataLayer.DataBaseConenction;
using Daw.DataLayer.Models;
using Daw.DataLayer.Repositories;
using Daw.DataLayer.Services;

namespace Daw.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getProducts", Name = "GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products= await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpPost("addProduct", Name = "AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
           await _productService.AddProductAsync(product);
            return Ok();
        }
        [HttpPut("updateProduct", Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(string name, Product product)
        {
          await _productService.UpdateProductAsync(product);
            return Ok();
        }
        [HttpDelete("deleteProduct", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
           await _productService.DeleteProductAsync(id);
            return Ok();
        }

    }
}
