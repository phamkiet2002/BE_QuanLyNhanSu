using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;
using static QuanLyNhanSu.Contract.Service.V1.Employee.Response;
using static QuanLyNhanSu.Contract.Service.V1.LeaveRegistration.Response;

namespace QuanLyNhanSu.Application.Mapper;
public class LeaveRegistrationMapper : Profile
{
    public LeaveRegistrationMapper()
    {
        // LeaveRegistration
        CreateMap<LeaveRegistration, Contract.Service.V1.LeaveRegistration.Response.LeaveRegistrationTypeOfLeaveDayOffResponse>().ReverseMap();
        CreateMap<PagedResult<LeaveRegistration>, PagedResult<Contract.Service.V1.LeaveRegistration.Response.LeaveRegistrationTypeOfLeaveDayOffResponse>>().ReverseMap();

        CreateMap<LeaveRegistration, Contract.Service.V1.LeaveRegistration.Response.LeaveRegistrationTypeOfLeaveHalfDayOffResponse>().ReverseMap();
        CreateMap<PagedResult<LeaveRegistration>, PagedResult<Contract.Service.V1.LeaveRegistration.Response.LeaveRegistrationTypeOfLeaveHalfDayOffResponse>>().ReverseMap();

        CreateMap<Domain.Entities.LeaveRegistration, LeaveRegistrationResponse>()
            .ForMember(dest => dest.Approval, opt => opt.MapFrom(src => src.Approval))
            .ReverseMap();
        CreateMap<PagedResult<Domain.Entities.LeaveRegistration>, PagedResult<LeaveRegistrationResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();
    }
}
