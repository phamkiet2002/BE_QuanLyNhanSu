using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class PositionMapper : Profile
{
    public PositionMapper()
    {
        // Position
        CreateMap<Position, Contract.Service.V1.Position.Response.PositionResponse>().ReverseMap();
        CreateMap<PagedResult<Position>, PagedResult<Contract.Service.V1.Position.Response.PositionResponse>>().ReverseMap();
    }
}
