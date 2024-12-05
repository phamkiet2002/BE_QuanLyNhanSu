using System.Linq;
using AutoMapper;
using QuanLyNhanSu.Application.UserCases.V1.Queries.Employee;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Employee;
using QuanLyNhanSu.Domain.Entities;
using static QuanLyNhanSu.Contract.Service.V1.Employee.Response;

namespace QuanLyNhanSu.Application.Mapper;
public class EmployeeMapper : Profile
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeMapper()
    {
        // Employee
        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeResponse>().ReverseMap();
        CreateMap<PagedResult<Employee>, PagedResult<Contract.Service.V1.Employee.Response.EmployeeResponse>>().ReverseMap();

        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeMapToContractResponse>();
        CreateMap<PagedResult<Employee>, PagedResult<Contract.Service.V1.Employee.Response.EmployeeMapToContractResponse>>();

        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeMapToLeaveRegistraionResponse>();
        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeMapApprovalResponse>();

        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeMapToAttendanceResponse>();
        CreateMap<PagedResult<Employee>, PagedResult<Contract.Service.V1.Employee.Response.EmployeeMapToAttendanceResponse>>();

        //CreateMap<Employee, EmployeePayRollResponse>()
        //    .ForMember(dest => dest.StandardWorkingDay, opt => opt.MapFrom(src => _employeeRepository.CalculateStandardWorkingDay(src)))
        //    .ForMember(dest => dest.TotalAllowance, opt => opt.MapFrom(src => _employeeRepository.CalculateTotalAllowance(src)))
        //    .ForMember(dest => dest.TotalPenalties, opt => opt.MapFrom(src => _employeeRepository.CalculateTotalPenalties(src)))
        //    .ReverseMap();
        //CreateMap<PagedResult<Employee>, PagedResult<EmployeePayRollResponse>>()
        //     .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
        //     .ReverseMap();

        CreateMap<Employee, EmployeePayRollResponse>()
            .ForMember(dest => dest.StandardWorkingDay, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAllowance, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPenalties, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<PagedResult<Employee>, PagedResult<EmployeePayRollResponse>>()
             .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
             .ReverseMap();
    }

    public EmployeeMapper(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
}
