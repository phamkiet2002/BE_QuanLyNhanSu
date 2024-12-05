using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class SalaryMapper : Profile
{
    public SalaryMapper()
    {
        // Salary
        CreateMap<Salary, Contract.Service.V1.Salary.Response.SalaryResponse>().ReverseMap();
        CreateMap<PagedResult<Salary>, PagedResult<Contract.Service.V1.Salary.Response.SalaryResponse>>().ReverseMap();
    }
}
