using System.Data;
using QuanLyNhanSu.Domain.Abstractions.Entities;

namespace QuanLyNhanSu.Domain.Entities;
public class Notification : DomainEntity<Guid>
{
    //public string? Role { get; set; }
    public Guid? EmployeeId { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public bool? IsRead { get; set; } = false;
    public string? Url { get; set; }
    public bool? TypePage { get; set; }
    public DateTime? CreatedAt { get; set; }

    public Notification() { }

    public Notification(Guid employeeId, string message, string url, string title, bool typePage)
    {
        EmployeeId = employeeId;
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Url = url;
        Title = title;
        TypePage = typePage;
        CreatedAt = DateTime.Now;
        IsRead = false;
    }

    public static Notification CreateNotification(Guid employeeId, string message, string url, string title, bool typePage)
    {
        return new Notification(employeeId, message, url, title, typePage);
    }


    public void MarkAsRead()
    {
        IsRead = true;
    }
}
