using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AttendanceSetting;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;
using QuanLyNhanSu.Domain.Enumerations;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.AttendanceSetting;
public sealed class DeleteAttendanceSettingCommandHandler : ICommandHandler<Command.DeleteAttendanceSettingCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> _attendanceSettingRepository;

    public DeleteAttendanceSettingCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.AttendanceSetting, Guid> attendanceSettingRepository)
    {
        _unitOfWork = unitOfWork;
        _attendanceSettingRepository = attendanceSettingRepository;
    }

    public async Task<Result> Handle(Command.DeleteAttendanceSettingCommand request, CancellationToken cancellationToken)
    {
        var attendanceSetting = await _attendanceSettingRepository.FindByIdAsync(request.Id)
            ?? throw new AttendanceSettingException.AttendanceSettingNotFoundException(request.Id);

        attendanceSetting.UpdateStatus(StatusEnums.Status.InActive);

        _attendanceSettingRepository.Update(attendanceSetting);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
