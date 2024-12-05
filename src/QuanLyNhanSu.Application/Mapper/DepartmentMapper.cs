using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class DepartmentMapper :Profile
{
    public DepartmentMapper()
    {
        // Department
        CreateMap<Department, Contract.Service.V1.Department.Response.DepartmentResponse>().ReverseMap();
        CreateMap<PagedResult<Department>, PagedResult<Contract.Service.V1.Department.Response.DepartmentResponse>>().ReverseMap();
    }
}
