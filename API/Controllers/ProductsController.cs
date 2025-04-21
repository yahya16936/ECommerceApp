
using ECommerceAppModels.ViewModels;
using Core.Services.Interface;
using Infrastructure.DataContext;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Specifications;
using Core.RequestHelpers;

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
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts(
            [FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
            return Ok(await _service.GetProducts(spec, specParams));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            return Ok(await (_service.GetProductById(id)));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await _service.GetBrands());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await _service.GetTypes());
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
            if (existingProduct == null || productDto.Id != id)
            {
                return NotFound($"Product with Id {id} not found.");
            }

            await _service.UpdateProduct(id, existingProduct);
            return Ok("Product updated successfully");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(ProductDto productDto)
        {
            var existingProduct = await (_service.GetProductById(productDto.Id));
            if (existingProduct == null)
            {
                return NotFound($"Product with Id {productDto.Id} not found.");
            }

            await _service.DeleteProduct(productDto);
            return Ok("Product deleted successfully.");
        }
    }
}
