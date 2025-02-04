using Microsoft.VisualBasic;

namespace QuanLyNhanSu.Contract.Service.V1.Notification;
public static class Response
{
    public record NotificationResponse(Guid Id, Guid EmployeeId, string Message, bool IsRead, string Url, string Title, bool TypePage, DateTime CreatedAt);
}
