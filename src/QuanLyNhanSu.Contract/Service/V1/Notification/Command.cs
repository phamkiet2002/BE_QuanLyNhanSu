using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.Notification;
public static class Command
{
    public record IsReadNotificationCommand(Guid Id) : ICommand;
}
