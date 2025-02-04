using AutoMapper;

namespace QuanLyNhanSu.Application.Mapper;
public class AppRoleMapper : Profile
{
    public AppRoleMapper()
    {
        CreateMap<Domain.Entities.Identity.AppRole, Contract.Service.V1.AppRole.Response.GetAppRoleResponse>();
    }
}
