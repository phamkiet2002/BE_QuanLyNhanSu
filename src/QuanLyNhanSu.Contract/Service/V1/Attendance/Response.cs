﻿namespace QuanLyNhanSu.Contract.Service.V1.Attendance;

public static class Response
{
    public record AttendanceResponse(Guid Id, DateTime CheckIn, DateTime CheckOut, bool IsLate, DateTime? LateTime,
        bool IsEarlyLeave, DateTime? EarlyLeaveTime , bool OvertimeOutsideWorkHours, DateTime? StartTime, DateTime? EndTime,
        Employee.Response.EmployeeMapApprovalResponse Approval);
}