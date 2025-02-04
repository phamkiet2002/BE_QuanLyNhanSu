using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;
using QuanLyNhanSu.Application.Abstractions;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Attendance;
public sealed class OvertimeOutsideWorkHourAttendanceCommandHandler : ICommandHandler<Command.OvertimeOutsideWorkHourAttendanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Attendance, Guid> _attendanceRepository;
    private readonly INotificationService _notificationService;

    public OvertimeOutsideWorkHourAttendanceCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Attendance, Guid> attendanceRepository, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _attendanceRepository = attendanceRepository;
        _notificationService = notificationService;
    }

    public async Task<Result> Handle(Command.OvertimeOutsideWorkHourAttendanceCommand request, CancellationToken cancellationToken)
    {
        var attendance = await _attendanceRepository.FindByIdAsync(request.Id)
            ?? throw new EmployeeException.EmployeeNotFoundException(request.Id);

        attendance.OvertimeOutsideWorkHoursCheckAttendance
        (
            request.StartTime,
            request.EndTime
        );

        var employeeIdNoti = attendance.EmployeeId;
        var title = "Ra ngoài quá giờ cho phép";
        var message = $"Bạn đã ra ngoài quá giờ cho phép ngày {attendance.CreatedDate.Value.ToString("dd/MM/yyyy")} thời gian từ {request.StartTime} - {request.EndTime}";
        var url = "/attendance";
        // Gửi thông báo
        await _notificationService.SendNotificationToUsers(employeeIdNoti.Value, message, url, title);


        _attendanceRepository.Update(attendance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
