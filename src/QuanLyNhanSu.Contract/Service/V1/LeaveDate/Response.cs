using Microsoft.VisualBasic;

namespace QuanLyNhanSu.Contract.Service.V1.LeaveDate;
public static class Response
{
    public record LeaveDateResponse(Guid Id, string Name, int TotalAnnualLeaveDate, 
        int MaximumDaysOffPerMonth, string Description, bool IsHoliday, DateTime CreatedDate, DateTime StartDate, DateTime EndDate);
}
