using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Application.Mapper;

public class AttendanceSettingMapper : Profile
{
    public AttendanceSettingMapper()
    {
        CreateMap<Domain.Entities.AttendanceSetting, Contract.Service.V1.AttendanceSetting.Response.AttendanceSettingResponse>().ReverseMap();
        CreateMap<PagedResult<Domain.Entities.AttendanceSetting>, PagedResult<Contract.Service.V1.AttendanceSetting.Response.AttendanceSettingResponse>>().ReverseMap();
    }
}
