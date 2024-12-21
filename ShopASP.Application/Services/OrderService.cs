using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Application.Interface;
using ShopASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;
        public OrderService(IOrderRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }
        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            IEnumerable<Order> orders = await repository.GetAll();
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<OrderDTO> GetByID(int id)
        {
            Order order = await repository.GetByID(id);
            return mapper.Map<OrderDTO>(order);
        }
        public async Task Create(OrderDTO entity)
        {
            Order order = mapper.Map<Order>(entity);
            await repository.Create(order);
        }
        public Task Update(OrderDTO orderDTO)
        {
            Order order = mapper.Map<Order>(orderDTO);
            return repository.Update(order);
        }
        public Task Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}
