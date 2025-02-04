using Microsoft.VisualBasic;

namespace QuanLyNhanSu.Contract.Service.V1.WorkShedule;
public static class Response
{
    public record WorkSheduleResponse(Guid Id, string Name, TimeSpan StartTime, TimeSpan EndTime, TimeSpan BreakStartTime, TimeSpan BreakEndTime, DateTime CreatedDate);
}
