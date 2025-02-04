using QuanLyNhanSu.Contract.Abstractions.Message;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;
using static QuanLyNhanSu.Domain.Enumerations.LeaveRegistrationEnums;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;

public static class Command
{
    public record CreateLeaveRegistrationNghiNgayCommand(DateTime StartDate, DateTime EndDate, string LeaveReason, Guid LeaveDateId): ICommand;
    public record CreateLeaveRegistrationNghiBuoiCommand(HalfDayOff HalfDayOff, DateTime DayOff, string LeaveReason, Guid? EmployeeId, Guid LeaveDateId) : ICommand;
    public record CancelLeaveRegistrationCommand(Guid Id) : ICommand;
    public record ApproveLeaveRegistrationCommand(Guid Id, string? ApprovalNote, PendingApproval? PendingApproval) : ICommand;
}
