using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.Position;
public static class Command
{
    public record CreatePositionCommand(string Name, string Description, List<string> RoleIds) : ICommand;
                        
    public record UpdatePositionCommand(Guid Id, string Name, string Description, List<string> RoleIds) : ICommand;
                        
    public record DeletePositionCommand(Guid Id) : ICommand;
}
