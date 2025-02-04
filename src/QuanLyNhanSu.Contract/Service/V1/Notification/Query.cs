using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.Notification;
public static class Query
{
    public record GetNotificationByIdQuery() : IQuery<List<Response.NotificationResponse>>;
}
