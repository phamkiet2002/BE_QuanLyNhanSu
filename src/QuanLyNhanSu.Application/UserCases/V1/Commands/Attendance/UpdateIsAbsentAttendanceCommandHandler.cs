using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Attendance;
public sealed class UpdateIsAbsentAttendanceCommandHandler : ICommandHandler<Command.UpdateIsAbsentAttendanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Attendance, Guid> _attendanceRepository;

    public UpdateIsAbsentAttendanceCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository)
    {
        _unitOfWork = unitOfWork;
        _attendanceRepository = attendanceRepository;
    }

    public async Task<Result> Handle(Command.UpdateIsAbsentAttendanceCommand request, CancellationToken cancellationToken)
    {
        var attendance = await _attendanceRepository.FindByIdAsync(request.Id)
            ?? throw new AttendanceException.AttendanceNotFoundException(request.Id);
        attendance.UpdateIsAbsentAttendance(request.IsAbsent);
        _attendanceRepository.Update(attendance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
