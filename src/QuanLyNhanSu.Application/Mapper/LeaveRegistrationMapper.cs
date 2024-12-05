using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

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
    }
}
