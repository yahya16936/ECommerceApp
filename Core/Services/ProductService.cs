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
        
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _repository.GetProductById(id);
            return _mapper.Map<ProductDto>(product);
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
