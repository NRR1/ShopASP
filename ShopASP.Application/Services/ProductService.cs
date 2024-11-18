using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;

namespace ShopASP.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IProductReposutory productRepository;

        public ProductService(IMapper _mapper, IProductReposutory _productReposutory)
        {
            mapper = _mapper;
            productRepository = _productReposutory;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }
        public async Task<ProductDTO> GetByIDAsync(int id)
        {
            var product = await productRepository.GetByIDAsync(id);
            return mapper.Map<ProductDTO>(product);
        }
        public Task CreateAsync(ProductDTO entity)
        {
            var product = mapper.Map<Product>(entity);
            return productRepository.CreateAsync(product);
        }
        public Task UpdateProduct(ProductDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            return productRepository.UpdateProduct(product);
        }
        public Task DeleteAsync(int id)
        {
            return productRepository.DeleteAsync(id);
        }
    }
}
