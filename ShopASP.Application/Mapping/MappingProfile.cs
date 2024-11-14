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

            
        }
    }
}
