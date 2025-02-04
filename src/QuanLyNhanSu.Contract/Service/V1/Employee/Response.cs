using QuanLyNhanSu.Contract.Abstractions.Shared;

namespace QuanLyNhanSu.Contract.Service.V1.Employee;
public static class Response
{
    public record EmployeeResponse(Guid Id, string MaNV, string Name, string Email,
        string Phone, string IdentityCard, string Gender,
        DateTime DateOfBirth, string Address, DateTime JoinDate,
        string BankName, string BankAccountNumber, string Status,
        WorkPlace.Response.WorkPlaceResponse WorkPlace,
        List<EmployeeDepartment.Response.EmployeeDepartmentResponse> EmployeeDepartments,
        List<EmployeePosition.Response.EmployeePositionResponse> EmployeePositions,
        List<Salary.Response.SalaryResponse> Salarys,
        List<EmployeeLevel.Response.EmployeeLevelResponse> EmployeeLevels, List<PayRoll.Response.PayRollResponse> PayRolls, DateTime CreatedDate);

    public record EmployeeMapToContractResponse(Guid Id, string MaNV, string Name, string Email,
        string Phone, string IdentityCard, string Gender,
        DateTime DateOfBirth, string Address);

    public record EmployeeMapToLeaveRegistraionResponse(Guid Id, string MaNV, string Name, string Email,
        string Phone, string IdentityCard, string Gender,
        DateTime DateOfBirth, string Address);

    public record EmployeeMapApprovalResponse(Guid Id, string MaNV, string Name);

    public record EmployeeMapToAttendanceResponse(Guid Id, string MaNV, string Name,
        WorkPlace.Response.WorkPlaceResponse WorkPlace,
        List<Attendance.Response.AttendanceResponse>? Attendances,
        TimeSpan? TotalTimeAttendance = null, int? TotalDayLate = null, TimeSpan? TotalTimeDayLate = null, int? TotalDayEarlyLeave = null, TimeSpan? TotalTimeDayEarlyLeave = null, int? TotalDayAbsent = null
    );

    public record EmployeePayRollResponse(Guid Id, string MaNV, string Name, DateTime JoinDate, string BankName, string BankAccountNumber,
        WorkPlace.Response.WorkPlaceResponse WorkPlace,
        string DepartmentName, string PositionName, string LevelName,
        List<Salary.Response.SalaryResponse>? Salarys,
        List<PayRoll.Response.PayRollResponse>? PayRolls);

    public record EmployeeByIdLeaveRegistrationResponse(Guid Id, string MaNV, string Name, PagedResult<LeaveRegistration.Response.LeaveRegistrationResponse> LeaveRegistrations);
}
