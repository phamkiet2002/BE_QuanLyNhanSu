using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class EmployeePositionMapper : Profile
{
    public EmployeePositionMapper()
    {
        // EmployeePosition
        CreateMap<EmployeePosition, Contract.Service.V1.EmployeePosition.Response.EmployeePositionResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap();
        CreateMap<PagedResult<EmployeePosition>, PagedResult<Contract.Service.V1.EmployeePosition.Response.EmployeePositionResponse>>().ReverseMap();
    }
}
