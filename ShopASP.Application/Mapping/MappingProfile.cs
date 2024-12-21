using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .ForMember(dest => dest.OrderProductId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderID))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
                .ForMember(dest => dest.ProductCost, opt => opt.MapFrom(src => src.Product != null ? src.Product.Cost : 0))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
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
