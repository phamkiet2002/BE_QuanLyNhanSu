using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class AllowanceAndPenaltiesMapper : Profile
{
    public AllowanceAndPenaltiesMapper()
    {
        // AllowanceAndPenalties
        CreateMap<AllowanceAndPenalties, Contract.Service.V1.AllowanceAndPenalties.Response.AllowanceResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.TypeOfAllowance, opt => opt.MapFrom(src => src.TypeOfAllowance.ToString()))
            .ForCtorParam("IsAllWorkPlace", opt => opt.MapFrom(src => src.IsAllWorkPlace == true ? "Tất cả nơi làm việc" : null))
            .ReverseMap();
        CreateMap<AllowanceAndPenalties, Contract.Service.V1.AllowanceAndPenalties.Response.PenaltiesResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.TypeOfPenalty, opt => opt.MapFrom(src => src.TypeOfPenalty.ToString()))
            .ForCtorParam("IsAllWorkPlace", opt => opt.MapFrom(src => src.IsAllWorkPlace == true ? "Tất cả nơi làm việc" : null))
            .ReverseMap();

        CreateMap<PagedResult<AllowanceAndPenalties>, PagedResult<Contract.Service.V1.AllowanceAndPenalties.Response.AllowanceResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();
        CreateMap<PagedResult<AllowanceAndPenalties>, PagedResult<Contract.Service.V1.AllowanceAndPenalties.Response.PenaltiesResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();
    }
}
