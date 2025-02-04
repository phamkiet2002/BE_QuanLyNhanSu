using AutoMapper;

namespace QuanLyNhanSu.Application.Mapper;
public class WifiConfigMapper : Profile
{
    public WifiConfigMapper()
    {
        CreateMap<Domain.Entities.WifiConfig, Contract.Service.V1.WifiConfig.Response.WifiConfigResponse>();
    }
}
