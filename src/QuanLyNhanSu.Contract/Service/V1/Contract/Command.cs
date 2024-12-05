using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.Contract;
public static class Command
{
    public record CreateContractCommand(int ContracNumber, DateTime SignDate, DateTime EffectiveDate, DateTime ExpirationDate, Guid? EmployeeId) : ICommand;
    public record UpdateContractCommand(Guid Id, int ContracNumber, DateTime SignDate, DateTime EffectiveDate, DateTime ExpirationDate, Guid? EmployeeId) : ICommand;
    public record DeleteContractCommand(Guid Id) : ICommand;
    public record CheckContractNearExpirationCommand() : ICommand;
}
