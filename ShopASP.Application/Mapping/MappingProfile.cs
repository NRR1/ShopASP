using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShopASP.Application.DTO;
using ShopASP.Domain.Entities;

namespace ShopASP.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Order -> OrderDTO
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)) // Маппинг имени пользователя
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts)); // Маппинг продуктов в заказе

            // OrderProduct -> OrderProductDTO
            CreateMap<OrderProduct, OrderProductDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name)) // Название товара
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Cost)); // Стоимость товара

            // Product -> ProductDTO
            CreateMap<Product, ProductDTO>();

            // IdentityUser -> UserDTO
            CreateMap<IdentityUser, UserDTO>()
                .ForMember(dest => dest.dID, opt => opt.MapFrom(src => int.Parse(src.Id))) // Преобразование string ID в int
                .ForMember(dest => dest.dEmail, opt => opt.MapFrom(src => src.Email)) // Электронная почта
                .ForMember(dest => dest.dSurname, opt => opt.Ignore()) // Игнорируем, если таких полей нет
                .ForMember(dest => dest.dPathronomic, opt => opt.Ignore()) // Аналогично
                .ForMember(dest => dest.dRoleNames, opt => opt.Ignore()); // Роли можно настроить отдельно, если требуется

            // IdentityRole -> RoleDTO
            CreateMap<IdentityRole, RoleDTO>()
                .ForMember(dest => dest.dID, opt => opt.MapFrom(src => int.Parse(src.Id))) // Преобразование string ID в int
                .ForMember(dest => dest.dName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
