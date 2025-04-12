using AutoMapper;
using ECommerceAppModels.ViewModels;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services.Interface;
using Infrastructure.Repositories.Interface;
using Infrastructure.Models;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ProductDto>> GetProducts(string? brand, string? type, string? sort)
        {
            var products = await _repository.GetProducts();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                products = products.Where(p => p.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                products = products.Where(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            products = sort switch
            {
                "priceAsc" => products.OrderBy(x => x.Price),
                "priceDesc" => products.OrderByDescending(x => x.Price),
                _ => products.OrderBy(x => x.Name)
            };

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _repository.GetProductById(id);
            return _mapper.Map<ProductDto>(product);
        }
        public async Task<List<string>> GetBrands()
        {
            var productsByBrands = await _repository.GetBrands();
            return (productsByBrands);
        }

        public async Task<List<string>> GetTypes()
        {
            var productsByTypes = await _repository.GetTypes();
            return (productsByTypes);
        }
        public async Task CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repository.CreateProduct(product);  
        }
        public async Task UpdateProduct(int id, ProductDto productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _repository.UpdateProduct(id, productEntity);
        }

        public async Task DeleteProduct(int id)
        {
            await _repository.DeleteProduct(id);
        }
    }
}
