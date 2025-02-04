using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AttendanceSetting;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.AttendanceSetting;
public sealed class CreateAttendanceSettingCommandHandler : ICommandHandler<Command.CreateAttendanceSettingCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> _attendanceSettingRepository;

    public CreateAttendanceSettingCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> attendanceSettingRepository)
    {
        _unitOfWork = unitOfWork;
        _attendanceSettingRepository = attendanceSettingRepository;
    }

    public async Task<Result> Handle(Command.CreateAttendanceSettingCommand request, CancellationToken cancellationToken)
    {
        var updateAttendanceSetting = _attendanceSettingRepository.FindAll(x => x.Status == Domain.Enumerations.StatusEnums.Status.Active);
        foreach (var item in updateAttendanceSetting)
        {
            var updateStatusInActive = Domain.Entities.AttendanceSetting.UpdateStatusAttendanceSetting
            (
               item.Id,
               Domain.Enumerations.StatusEnums.Status.InActive
            );
            _attendanceSettingRepository.Update(updateStatusInActive);

        }
        var attendanceSetting = Domain.Entities.AttendanceSetting.CreateAttendanceSetting
         (
             Guid.NewGuid(),
             request.MaximumLateAllowed,
             request.MaxEarlyLeaveAllowed
         );
        _attendanceSettingRepository.Add(attendanceSetting);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
