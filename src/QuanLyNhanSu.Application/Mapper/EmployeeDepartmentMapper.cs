using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class EmployeeDepartmentMapper : Profile
{
    public EmployeeDepartmentMapper()
    {
        // EmployeeDepartment
        CreateMap<EmployeeDepartment, Contract.Service.V1.EmployeeDepartment.Response.EmployeeDepartmentResponse>()
          //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.ToString() : "Default"))
          .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
          .ReverseMap();
        CreateMap<PagedResult<EmployeeDepartment>, PagedResult<Contract.Service.V1.EmployeeDepartment.Response.EmployeeDepartmentResponse>>().ReverseMap();
    }
}
