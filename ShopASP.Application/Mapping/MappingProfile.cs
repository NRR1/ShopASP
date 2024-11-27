using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Domain.Entities;

namespace ShopASP.Application.Mapping
{
        //public class MappingProfile : Profile
        //{
        //    public MappingProfile()
        //    {
        //        // User <-> UserDTO
        //        CreateMap<User, UserDTO>()
        //            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id)) // IdentityUser<int> использует Id
        //            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        //            .ForMember(dest => dest.RoleIDs, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Id)))
        //            .ForMember(dest => dest.RoleNames, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name)));

        //        CreateMap<UserDTO, User>()
        //            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
        //            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        //            .ForMember(dest => dest.UserRoles, opt => opt.Ignore()); // Связи маппятся отдельно

        //        // Role <-> RoleDTO
        //        CreateMap<Role, RoleDTO>()
        //            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
        //            .ForMember(dest => dest.UserIDs, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.User.Id)));

        //        CreateMap<RoleDTO, Role>()
        //            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
        //            .ForMember(dest => dest.UserRoles, opt => opt.Ignore());

        //        // Product <-> ProductDTO
        //        CreateMap<Product, ProductDTO>().ReverseMap();

        //        // Order <-> OrderDTO
        //        CreateMap<Order, OrderDTO>()
        //            .ForMember(dest => dest.dUser, opt => opt.MapFrom(src => src.User.Name))
        //            .ForMember(dest => dest.dProduct, opt => opt.MapFrom(src => src.Product.Name));

        //        CreateMap<OrderDTO, Order>()
        //            .ForMember(dest => dest.User, opt => opt.Ignore()) // Связи маппятся отдельно
        //            .ForMember(dest => dest.Product, opt => opt.Ignore());
        //    }
        //}

}
