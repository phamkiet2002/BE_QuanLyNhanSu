using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class WorkPlaceAllowanceAndPenaltiesMapper : Profile
{
    public WorkPlaceAllowanceAndPenaltiesMapper()
    {
        // WorkPlaceAllowanceAndPenalties
        CreateMap<WorkPlaceAllowanceAndPenalties, Contract.Service.V1.WorkPlaceAllowanceAndPenalties.Response.WorkPlaceAllowanceResponse>().ReverseMap();
        CreateMap<WorkPlaceAllowanceAndPenalties, Contract.Service.V1.WorkPlaceAllowanceAndPenalties.Response.WorkPlacePenaltiesResponse>().ReverseMap();
        CreateMap<PagedResult<WorkPlaceAllowanceAndPenalties>, PagedResult<Contract.Service.V1.WorkPlaceAllowanceAndPenalties.Response.WorkPlaceAllowanceResponse>>().ReverseMap();
        CreateMap<PagedResult<WorkPlaceAllowanceAndPenalties>, PagedResult<Contract.Service.V1.WorkPlaceAllowanceAndPenalties.Response.WorkPlacePenaltiesResponse>>().ReverseMap();
    }
}
