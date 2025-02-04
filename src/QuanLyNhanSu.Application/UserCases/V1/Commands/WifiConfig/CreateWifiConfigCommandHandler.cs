using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WifiConfig;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions.Repositories.WifiConfig;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.WifiConfig;
public sealed class CreateWifiConfigCommandHandler : ICommandHandler<Command.CreateWifiConfigCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWifiCongfigRepository _wifiConfigRepository;
    private readonly IRepositoryBase<Domain.Entities.WifiConfig, Guid> _wifiConfigRepositoryBase;
    private readonly IRepositoryBase<Domain.Entities.WorkPlace, Guid> _workPlaceRepositoryBase;

    public CreateWifiConfigCommandHandler
    (
        IUnitOfWork unitOfWork,
        IWifiCongfigRepository wifiCongfigRepository,
        IRepositoryBase<Domain.Entities.WifiConfig, Guid> wifiConfigRepositoryBase,
        IRepositoryBase<Domain.Entities.WorkPlace, Guid> workPlaceRepositoryBase
    )
    {
        _unitOfWork = unitOfWork;
        _wifiConfigRepository = wifiCongfigRepository;
        _wifiConfigRepositoryBase = wifiConfigRepositoryBase;
        _workPlaceRepositoryBase = workPlaceRepositoryBase;
    }

    public async Task<Result> Handle(Command.CreateWifiConfigCommand request, CancellationToken cancellationToken)
    {

        var workPlaceId = await _workPlaceRepositoryBase.FindByIdAsync(request.WorkPlaceId)
            ?? throw new WorkPlaceException.WorkPlaceNotFoundException(request.WorkPlaceId);

        var existingWifiConfig = _wifiConfigRepositoryBase
             .FindAll(x => x.WorkPlaceId == workPlaceId.Id)
             .FirstOrDefault();

        if (existingWifiConfig != null)
        {
            existingWifiConfig.UpdateWifiConfig(request.SSID, request.BSSID);
            _wifiConfigRepositoryBase.Update(existingWifiConfig);
        }
        else
        {
            var wifiConfig = Domain.Entities.WifiConfig.CreateWifiConfig
            (
                Guid.NewGuid(),
                request.SSID,
                request.BSSID,
                request.WorkPlaceId
            );
            _wifiConfigRepositoryBase.Add(wifiConfig);
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
