using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Domain.Entities;

namespace ShopASP.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleDTO>()
               .ForMember(dest => dest.rID, opt => opt.MapFrom(src => src.ID))
               .ForMember(dest => dest.rName, opt => opt.MapFrom(src => src.Name));
            CreateMap<RoleDTO, Role>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.rID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.rName));

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.uID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.uName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.uSurname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.uPathronomic, opt => opt.MapFrom(src => src.Pathronomic))
                .ForMember(dest => dest.uLogin, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.uPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.uRoleID, opt => opt.MapFrom(src => src.RoleID))
                .ForMember(dest => dest.uRoleName, opt => opt.MapFrom(src => src.Roles));
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.uID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.uName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.uSurname))
                .ForMember(dest => dest.Pathronomic, opt => opt.MapFrom(src => src.uPathronomic))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.uLogin))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.uPassword))
                .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => src.uRoleID))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.uRoleName));

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.pID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.pName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.pDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.pCost, opt => opt.MapFrom(src => src.Cost))
                .ForMember(dest => dest.pQuantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.pOrders, opt => opt.MapFrom(src => src.Orders));
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.pID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.pName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.pDescription))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.pCost))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.pQuantity))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.pOrders));

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.dOrderID, opt => opt.MapFrom(src => src.OrderID))
                .ForMember(dest => dest.dUser, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.dProductID, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.dUser, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.dProduct, opt => opt.MapFrom(src => src.Product));
            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.dOrderID))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.dUserID))
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.dProductID))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.dUser))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.dProduct));

        }
    }
}
