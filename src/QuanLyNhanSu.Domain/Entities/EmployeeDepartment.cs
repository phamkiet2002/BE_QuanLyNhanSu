using QuanLyNhanSu.Domain.Abstractions.Entities;
using QuanLyNhanSu.Domain.Enumerations;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class EmployeeDepartment : DomainEntity<Guid>
{
    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }

    public Guid? DepartmentId { get; set; }
    public virtual Department Department { get; set; }

    public Status? Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? EndDate { get; set; }

    public EmployeeDepartment() { }

    public EmployeeDepartment(Guid? employeeId, Guid? departmentId)
    {
        EmployeeId = employeeId;
        DepartmentId = departmentId;
        Status = StatusEnums.Status.Active;
        CreatedDate = DateTime.Now;
    }

    public static EmployeeDepartment CreateEmployeeDepartment(Guid? employeeId, Guid? departmentId)
    {
        return new EmployeeDepartment(employeeId, departmentId);
    }

    public void UpdateEmployeeDepartment(Status? status, DateTime? endDate)
    {
        Status = status;
        EndDate = endDate;
    }
}
