using AutoMapper;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WifiConfig;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.WifiConfig;
using QuanLyNhanSu.Persistence.Repositories.WifiConfig;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.WifiConfig;
public sealed class GetInfoWifiQueryHandler : IQueryHandler<Query.GetWifiConfigQuery, Response.WifiConfigInforResponse>
{
    private readonly IMapper _mapper;
    private readonly IWifiCongfigRepository _wifiConfigRepository;
    private readonly IRepositoryBase<Domain.Entities.WifiConfig, Guid> _wifiConfigRepositoryBase;

    public GetInfoWifiQueryHandler
    (
        IMapper mapper,
        IWifiCongfigRepository wifiCongfigRepository,
        IRepositoryBase<Domain.Entities.WifiConfig, Guid> wifiConfigRepositoryBase
    )
    {
        _mapper = mapper;
        _wifiConfigRepository = wifiCongfigRepository;
        _wifiConfigRepositoryBase = wifiConfigRepositoryBase;
    }

    public async Task<Result<Response.WifiConfigInforResponse>> Handle(Query.GetWifiConfigQuery request, CancellationToken cancellationToken)
    {
        var wifiConfigInFor = _wifiConfigRepository.GetWiFiInfo()
            ?? throw new Exception("Không tìm thấy thông tin wifi");
        string ssid = _wifiConfigRepository.ExtractInfo(wifiConfigInFor, "SSID\\s+:\\s+(.+)");
        string bssid = _wifiConfigRepository.ExtractInfo(wifiConfigInFor, "BSSID\\s+:\\s+(.+)");

        return Result<Response.WifiConfigResponse>.Success(
            new Response.WifiConfigInforResponse(ssid, bssid)
        );
    }
}
