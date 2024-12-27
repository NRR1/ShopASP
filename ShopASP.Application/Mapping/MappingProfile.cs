using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Domain.Entities;

namespace ShopASP.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.dOrderID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.dUserID, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.dOrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.dTotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.dOrderProducts, opt => opt.MapFrom(src => src.OrderProducts))
                .ReverseMap();

            CreateMap<OrderProduct, OrderProductDTO>()
                .ForMember(dest => dest.dOrderProductId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.dOrderId, opt => opt.MapFrom(src => src.OrderID))
                .ForMember(dest => dest.dProductId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.dProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
                .ForMember(dest => dest.dProductCost, opt => opt.MapFrom(src => src.Product != null ? src.Product.Cost : 0))
                .ForMember(dest => dest.dQuantity, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.pdID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.pdName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.pdDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.pdCost, opt => opt.MapFrom(src => src.Cost))
                .ForMember(dest => dest.pdQuantity, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap();
        }
    }
}
