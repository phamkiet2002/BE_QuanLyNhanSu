namespace QuanLyNhanSu.Contract.Service.V1.AttendanceSetting;
public static class Response
{
    public record AttendanceSettingResponse(Guid Id, TimeSpan MaximumLateAllowed, TimeSpan MaxEarlyLeaveAllowed, string Status, DateTime CreatedDate);

}
