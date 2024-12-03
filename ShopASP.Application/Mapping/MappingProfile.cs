using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Domain.Entities;

namespace ShopASP.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //CreateMap<Role, RoleDTO>()
            //    .ForMember(dest => dest.dID, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.dName, opt => opt.MapFrom(src => src.Name));

            //CreateMap<RoleDTO, Role>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.dID))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.dName));


            //CreateMap<User, UserDTO>()
            //    .ForMember(dest => dest.dID, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.dSurname, opt => opt.MapFrom(src => src.Surname))
            //    .ForMember(dest => dest.dPathronomic, opt => opt.MapFrom(src => src.Pathronomic))
            //    .ForMember(dest => dest.dEmail, opt => opt.MapFrom(src => src.Email));

            //CreateMap<UserDTO, User>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.dID))
            //    .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.dSurname))
            //    .ForMember(dest => dest.Pathronomic, opt => opt.MapFrom(src => src.dPathronomic))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.dEmail));


            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.pID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.pName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.pDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.pCost, opt => opt.MapFrom(src => src.Cost))
                .ForMember(dest => dest.pQuantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.pID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.pName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.pDescription))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.pCost))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.pQuantity));


            //CreateMap<Order, OrderDTO>()
            //    .ForMember(dest => dest.dOrderID, opt => opt.MapFrom(src => src.OrderID))
            //    .ForMember(dest => dest.dUserID, opt => opt.MapFrom(src => src.UserID))
            //    .ForMember(dest => dest.dProductID, opt => opt.MapFrom(src => src.ProductID))
            //    .ForMember(dest => dest.dUserName, opt => opt.MapFrom(src => src.User))
            //    .ForMember(dest => dest.dProductName, opt => opt.MapFrom(src => src.Product));

            //CreateMap<OrderDTO, Order>()
            //    .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.dOrderID))
            //    .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.dUserID))
            //    .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.dProductID))
            //    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.dUserName))
            //    .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.dProductName));

        }
    }
}
