using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class WorkPlaceMapper : Profile
{
    public WorkPlaceMapper()
    {
        // WorkPlace
        CreateMap<WorkPlace, Contract.Service.V1.WorkPlace.Response.WorkPlaceResponse>().ReverseMap();
        CreateMap<PagedResult<WorkPlace>, PagedResult<Contract.Service.V1.WorkPlace.Response.WorkPlaceResponse>>().ReverseMap();
    }
}
