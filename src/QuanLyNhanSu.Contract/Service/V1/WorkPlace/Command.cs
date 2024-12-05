using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.WorkPlace;
public static class Command
{
    public record CreateWorkPlaceCommand(string Name, string Phone, string Email, string Address) : ICommand;

    public record UpdateWorkPlaceCommand(Guid Id, string Name, string Phone, string Email, string Address) : ICommand;

    public record DeleteWorkPlaceCommand(Guid Id) : ICommand;
}
