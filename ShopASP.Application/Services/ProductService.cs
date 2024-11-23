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
            try
            {
                return mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public async Task<ProductDTO> GetByIDAsync(int id)
        {
            var product = await productRepository.GetByIDAsync(id);
            try
            {
                return mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public Task CreateAsync(ProductDTO entity)
        {
            var product = mapper.Map<Product>(entity);
            try
            {
                return productRepository.CreateAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public Task UpdateProduct(ProductDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            try
            {
                return productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public Task DeleteAsync(int id)
        {
            try
            {
                return productRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
    }
}
