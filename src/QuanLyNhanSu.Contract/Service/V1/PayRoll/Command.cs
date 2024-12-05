using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.PayRoll;
public static class Command
{
    public record CreatePayRollCommand() : ICommand;
    public record UpdatePayRollCommand(Guid Id, DateTime FromDate, DateTime ToDate, decimal SalaryGross, decimal SalaryNet, string PayRollStatus, DateTime PaymentDate) : ICommand;
    public record DeletePayRollCommand(Guid Id) : ICommand;

    public record CalculatePayrollResponseCommand() : ICommand;
}
