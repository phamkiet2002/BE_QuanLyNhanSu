using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Contract.Service.V1.EmployeeDepartment;

public static class Response
{
    public record EmployeeDepartmentResponse(Department.Response.DepartmentResponse Department, string Status, DateTime CreatedDate);
}
