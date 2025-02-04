using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Attendance;
public sealed class CreateCheckInCheckOutAttendanceCommandHandler : ICommandHandler<Command.CreateCheckInCheckOutAttendanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Attendance, Guid> _attendanceRepository;

    public CreateCheckInCheckOutAttendanceCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository)
    {
        _unitOfWork = unitOfWork;
        _attendanceRepository = attendanceRepository;
    }

    public async Task<Result> Handle(Command.CreateCheckInCheckOutAttendanceCommand request, CancellationToken cancellationToken)
    {
        var attendance = Domain.Entities.Attendance.CreateCheckInCheckOutAttendance
        (
            Guid.NewGuid(),
            request.CheckIn,
            request.CheckOut,
            null,
            null,
            null,
            null,
            null,
            request.ApprovalNote,
            request.EmployeeId
        );
        _attendanceRepository.Add(attendance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
