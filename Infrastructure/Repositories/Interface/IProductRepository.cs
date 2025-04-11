using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;


namespace Infrastructure.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task CreateProduct(Product product);
        Task UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);
    }
}
