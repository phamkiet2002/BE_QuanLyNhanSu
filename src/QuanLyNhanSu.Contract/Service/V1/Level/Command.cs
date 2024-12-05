using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.Level;
public static class Command
{
    public record CreateLevelCommand(string Name, string Description) : ICommand;

    public record UpdateLevelCommand(Guid Id, string Name, string Description) : ICommand;

    public record DeleteLevelCommand(Guid Id) : ICommand;
}
