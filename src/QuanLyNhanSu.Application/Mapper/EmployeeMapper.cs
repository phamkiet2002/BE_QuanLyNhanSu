using System.Linq;
using AutoMapper;
using QuanLyNhanSu.Application.UserCases.V1.Queries.Employee;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Abstractions.Repositories.Employee;
using QuanLyNhanSu.Domain.Entities;
using static QuanLyNhanSu.Contract.Service.V1.Employee.Response;
using static QuanLyNhanSu.Contract.Service.V1.LeaveRegistration.Response;

namespace QuanLyNhanSu.Application.Mapper;
public class EmployeeMapper : Profile
{
    public EmployeeMapper()
    {
        // Employee
        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeResponse>().ReverseMap();
        CreateMap<PagedResult<Employee>, PagedResult<Contract.Service.V1.Employee.Response.EmployeeResponse>>().ReverseMap();

        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeMapToContractResponse>();
        CreateMap<PagedResult<Employee>, PagedResult<Contract.Service.V1.Employee.Response.EmployeeMapToContractResponse>>();

        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeMapToLeaveRegistraionResponse>();
        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeMapApprovalResponse>().ReverseMap();

        CreateMap<Employee, Contract.Service.V1.Employee.Response.EmployeeMapToAttendanceResponse>()
            .ForMember(dest => dest.TotalTimeAttendance, opt => opt.Ignore())
            .ForMember(dest => dest.TotalDayLate, opt => opt.Ignore())
            .ForMember(dest => dest.TotalTimeDayLate, opt => opt.Ignore())
            .ForMember(dest => dest.TotalDayEarlyLeave, opt => opt.Ignore())
            .ForMember(dest => dest.TotalTimeDayEarlyLeave, opt => opt.Ignore())
            .ForMember(dest => dest.TotalDayAbsent, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<PagedResult<Employee>, PagedResult<Contract.Service.V1.Employee.Response.EmployeeMapToAttendanceResponse>>();

        CreateMap<Employee, EmployeePayRollResponse>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.EmployeeDepartments.FirstOrDefault(x=> x.Status == Domain.Enumerations.StatusEnums.Status.Active).Department.Name))
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.EmployeePositions.FirstOrDefault(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active).Position.Name))
            .ForMember(dest => dest.LevelName, opt => opt.MapFrom(src => src.EmployeeLevels.FirstOrDefault(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active).Level.Name))
            .ForMember(dest => dest.Salarys, opt => opt.MapFrom(src => src.Salarys))
            .ForMember(dest => dest.PayRolls, opt => opt.MapFrom(src => src.PayRolls))
            .ReverseMap();
        CreateMap<PagedResult<Employee>, PagedResult<EmployeePayRollResponse>>()
             .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
             .ReverseMap();

        //CreateMap<Employee, EmployeeByIdLeaveRegistrationResponse>().ReverseMap();
        CreateMap<Domain.Entities.Employee, EmployeeByIdLeaveRegistrationResponse>()
            .ForMember(dest => dest.LeaveRegistrations, opt => opt.MapFrom(src => src.LeaveRegistrations))
            .ReverseMap();
    }
}
