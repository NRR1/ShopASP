using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopASP.Application.DTO;
using ShopASP.Application.Interface;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;

public class ProductService : IProductService
{
    private readonly IMapper mapper;
    private readonly IProductRepository repository;

    public ProductService(IMapper _mapper, IProductRepository _repository)
    {
        mapper = _mapper;
        repository = _repository;
    }

    public async Task<IEnumerable<ProductDTO>> GetAll()
    {
        IEnumerable<Product> products = await repository.GetAll();
        return mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<ProductDTO> GetByID(int id)
    {
        Product product = await repository.GetByID(id);
        return mapper.Map<ProductDTO>(product);
    }

    public async Task Create(ProductDTO entity)
    {
        Product product = mapper.Map<Product>(entity);
        await repository.Create(product);
    }

    public async Task Update(ProductDTO productDTO)
    {
        Product product = mapper.Map<Product>(productDTO);

        await repository.Update(product);
    }


    public async Task Delete(int id)
    {
        await repository.Delete(id);
    }
}
