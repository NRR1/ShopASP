using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Application.Interface;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Repositories;

public class OrderService : IOrderService
{
    private readonly IOrderRepository repository;
    private readonly IProductRepository productRepository;
    private readonly IOrderProductRepository orderProductRepository;
    private readonly IMapper mapper;

    public OrderService(IOrderRepository _repository, IProductRepository _productRepository, IOrderProductRepository _orderProductRepository, IMapper _mapper)
    {
        orderProductRepository = _orderProductRepository;
        repository = _repository;
        productRepository = _productRepository;
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

    public async Task Create(OrderProductDTO orderDTO)
    {
        // Создаем заказ
        OrderProduct order = mapper.Map<OrderProduct>(orderDTO);
        await orderProductRepository.Create(order); // Сохраняем заказ

        // Добавляем записи в таблицу OrderProduct
        foreach (var orderProductDTO in orderDTO.dOrderProducts)
        {
            var orderProduct = new OrderProduct
            {
                OrderID = order.OrderID,  // Присваиваем ID созданного заказа
                ProductID = orderProductDTO.dProductId,
                Quantity = orderProductDTO.dQuantity
            };
            await orderProductRepository.Create(orderProduct); // Сохраняем связь
        }
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
