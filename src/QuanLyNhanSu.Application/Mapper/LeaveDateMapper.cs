using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class LeaveDateMapper : Profile
{
    public LeaveDateMapper()
    {
        // LeaveDate
        CreateMap<LeaveDate, Contract.Service.V1.LeaveDate.Response.LeaveDateResponse>().ReverseMap();
        CreateMap<PagedResult<LeaveDate>, PagedResult<Contract.Service.V1.LeaveDate.Response.LeaveDateResponse>>().ReverseMap();
    }
}
