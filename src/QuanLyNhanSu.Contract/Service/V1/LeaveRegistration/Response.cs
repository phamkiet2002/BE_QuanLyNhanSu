namespace QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;

public static class Response
{
    public record LeaveRegistrationTypeOfLeaveDayOffResponse(Guid Id, string TypeOfLeave, 
        DateTime StartDate, DateTime EndDate, string LeaveReason,
        string PendingApproval, DateTime ApprovedTime,
        Employee.Response.EmployeeMapToLeaveRegistraionResponse Employee,
        Employee.Response.EmployeeMapApprovalResponse Approval,
        LeaveDate.Response.LeaveDateResponse LeaveDate, DateTime CreatedDate);

    public record LeaveRegistrationTypeOfLeaveHalfDayOffResponse(Guid Id, string TypeOfLeave,
        string HalfDayoff, DateTime DayOff, string LeaveReason,
        string PendingApproval, DateTime ApprovedTime,
        Employee.Response.EmployeeMapToLeaveRegistraionResponse Employee,
        Employee.Response.EmployeeMapApprovalResponse Approval,
        LeaveDate.Response.LeaveDateResponse LeaveDate, DateTime CreatedDate);
}
