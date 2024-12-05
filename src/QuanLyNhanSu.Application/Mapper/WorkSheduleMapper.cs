using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;
namespace QuanLyNhanSu.Application.Mapper;
public class WorkSheduleMapper : Profile
{
    public WorkSheduleMapper()
    {
        // WorkShedule
        CreateMap<WorkShedule, Contract.Service.V1.WorkShedule.Response.WorkSheduleResponse>().ReverseMap();
        CreateMap<PagedResult<WorkShedule>, PagedResult<Contract.Service.V1.WorkShedule.Response.WorkSheduleResponse>>().ReverseMap();
    }
}
