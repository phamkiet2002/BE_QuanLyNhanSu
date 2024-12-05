using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveDate;
public static class Command
{
    public record CreateLeaveDateCommand(string Name, int TotalAnnualLeaveDate, int MaximumDaysOffPerMonth, string Description) : ICommand;

    public record UpdateLeaveDateCommand(Guid Id, string Name, int TotalAnnualLeaveDate, int MaximumDaysOffPerMonth, string Description) : ICommand;

    public record DeleteLeaveDateCommand(Guid Id) : ICommand;
}
