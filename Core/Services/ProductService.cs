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
using Infrastructure.Data;
using Infrastructure.Specifications;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ProductDto>> GetProducts(string? brand, string? type, string? sort)
        {
            var spec = new ProductSpecification(brand, type, sort);

            var products = await _productRepository.ListAsync(spec);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return _mapper.Map<ProductDto>(product);
        }
        public async Task<List<string>> GetBrands()
        {
            var spec = new BrandListSpecification();

            return (await _productRepository.ListAsync(spec)).ToList();
        }

        public async Task<List<string>> GetTypes()
        {
            var spec = new TypeListSpecification();

            return (await _productRepository.ListAsync(spec)).ToList();
        }
        public async Task CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productRepository.Add(product);  
        }
        public async Task UpdateProduct(int id, ProductDto productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            _productRepository.Update(productEntity);
        }

        public async Task DeleteProduct(ProductDto productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            _productRepository.Delete(productEntity);
        }
    }
}
