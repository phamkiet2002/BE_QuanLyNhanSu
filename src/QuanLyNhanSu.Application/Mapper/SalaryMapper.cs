using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class SalaryMapper : Profile
{
    public SalaryMapper()
    {
        // Salary
        CreateMap<Salary, Contract.Service.V1.Salary.Response.SalaryResponse>()
            .ForMember(dest => dest.Salarys, opt => opt.MapFrom(src => src.Salarys))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap();
        CreateMap<PagedResult<Salary>, PagedResult<Contract.Service.V1.Salary.Response.SalaryResponse>>().ReverseMap();
    }
}
