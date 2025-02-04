using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class EmployeeLevelMapper : Profile
{
    public EmployeeLevelMapper()
    {
        // EmployeeLevel
        CreateMap<EmployeeLevel, Contract.Service.V1.EmployeeLevel.Response.EmployeeLevelResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap();
        CreateMap<PagedResult<EmployeeLevel>, PagedResult<Contract.Service.V1.EmployeeLevel.Response.EmployeeLevelResponse>>().ReverseMap();
    }
}
