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
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int id);
        Task CreateProduct(ProductDto product);
        Task UpdateProduct(int id, ProductDto productDto); 
        Task DeleteProduct(int id);
    }
}
