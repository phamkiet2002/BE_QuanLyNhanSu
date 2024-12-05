using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class LevelMapper : Profile
{
    public LevelMapper()
    {
        // Level
        CreateMap<Level, Contract.Service.V1.Level.Response.LevelResponse>().ReverseMap();
        CreateMap<PagedResult<Level>, PagedResult<Contract.Service.V1.Level.Response.LevelResponse>>().ReverseMap();
    }
}
