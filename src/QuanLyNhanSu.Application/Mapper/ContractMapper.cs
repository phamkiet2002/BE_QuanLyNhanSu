using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Application.Mapper;
public class ContractMapper : Profile
{
    public ContractMapper()
    {
        // Contract
        CreateMap<Domain.Entities.Contract, Contract.Service.V1.Contract.Response.ContractResponse>().ReverseMap();
        CreateMap<PagedResult<Domain.Entities.Contract>, PagedResult<Contract.Service.V1.Contract.Response.ContractResponse>>().ReverseMap();
    }
}
