namespace QuanLyNhanSu.Application.Abstractions;
public interface INotificationService
{
    public Task SendNotificationToAdmins(string message, List<string> roles, string url, string title);
    public Task SendNotificationToUsers(Guid employeeId, string message, string url, string title);
}
