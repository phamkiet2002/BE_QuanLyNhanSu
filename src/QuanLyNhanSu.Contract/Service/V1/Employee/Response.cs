namespace QuanLyNhanSu.Contract.Service.V1.Employee;
public static class Response
{
    public record EmployeeResponse(Guid Id, string MaNv, string Name, string Email,
        string Phone, string IdentityCard, string Gender,
        DateTime DateOfBirth, string Address, DateTime JoinDate,
        string BankName, string BankAccountNumber,
        WorkPlace.Response.WorkPlaceResponse WorkPlace,
        List<EmployeeDepartment.Response.EmployeeDepartmentResponse> EmployeeDepartments,
        List<EmployeePosition.Response.EmployeePositionResponse> EmployeePositions,
        List<Salary.Response.SalaryResponse> Salarys,
        List<EmployeeLevel.Response.EmployeeLevelResponse> EmployeeLevels, List<PayRoll.Response.PayRollResponse> PayRolls, DateTime CreatedDate);

    public record EmployeeMapToContractResponse(Guid Id, string MaNv, string Name, string Email,
        string Phone, string IdentityCard, string Gender,
        DateTime DateOfBirth, string Address);

    public record EmployeeMapToLeaveRegistraionResponse(Guid Id, string MaNv, string Name, string Email,
        string Phone, string IdentityCard, string Gender,
        DateTime DateOfBirth, string Address);

    public record EmployeeMapApprovalResponse(Guid Id, string MaNv, string Name);

    public record EmployeeMapToAttendanceResponse(Guid Id, string MaNv, string Name, string Email,
        string Phone, string IdentityCard, string Gender,
        DateTime DateOfBirth, string Address, List<Attendance.Response.AttendanceResponse> Attendances);

    public record EmployeePayRollResponse(Guid Id, string MaNv, string Name, List<Salary.Response.SalaryResponse> Salarys, List<PayRoll.Response.PayRollResponse> PayRolls,
        double StandardWorkingDay, decimal TotalAllowance, decimal TotalPenalties);
}
