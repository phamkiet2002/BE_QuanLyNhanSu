using AutoMapper;

namespace QuanLyNhanSu.Application.Mapper;
public class PositionRoleMapper : Profile
{
    public PositionRoleMapper()
    {
        CreateMap<Domain.Entities.Identity.PositionRole, Contract.Service.V1.PositionRole.Response.PositionRoleResponse>()
            .ForMember(dest => dest.AppRole, opt => opt.MapFrom(src => src.AppRole));
    }
}
