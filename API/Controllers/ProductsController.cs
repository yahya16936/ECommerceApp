
using ECommerceAppModels.ViewModels;
using Core.Services.Interface;
using Infrastructure.DataContext;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            return Ok(await _service.GetProducts());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            return Ok(await (_service.GetProductById(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto product)
        {
            await _service.CreateProduct(product);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            Console.WriteLine();
            var existingProduct = await _service.GetProductById(id);
            if(existingProduct == null || productDto.Id != id)
            {
                return NotFound($"Product with Id {id} not found.");
            }
            
            await _service.UpdateProduct(id, existingProduct);
            return Ok("Product updated successfully");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await (_service.GetProductById(id));
            if (existingProduct == null)
            {
                return NotFound($"Product with Id {id} not found.");
            }

            await _service.DeleteProduct(id);
            return Ok("Product deleted successfully.");
        }
    }
}
