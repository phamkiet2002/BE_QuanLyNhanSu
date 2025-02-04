using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.Account;
public static class Command
{
    public record RegisterCommand(string UserName, string Email, string Password) : ICommand;
    public record LoginCommand(string Email, string Password) : ICommand<Response.LoginResponse>;
    public record LogoutCommand() : ICommand;
    public record ChangePasswordCommand(string Password, string NewPassword) : ICommand;
}
