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
using Core.RequestHelpers;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IPaginationService _paginationService;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository,
            IMapper mapper, IPaginationService paginationService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _paginationService = paginationService;
        }
        
        public async Task<Pagination<ProductDto>> GetProducts(ProductSpecification spec, ProductSpecParams specParams)
        {

            return await _paginationService.CreateAsync<Product, ProductDto>(
                spec,
                _productRepository,
                specParams.PageIndex,
                specParams.PageSize
            );
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
