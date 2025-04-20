using ECommerceAppModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts(string? brand, string? type, string? sort);
        Task<ProductDto> GetProductById(int id);
        Task<List<string>> GetBrands();
        Task<List<string>> GetTypes();
        Task CreateProduct(ProductDto product);
        Task UpdateProduct(int id, ProductDto productDto); 
        Task DeleteProduct(ProductDto productDto);
    }
}
