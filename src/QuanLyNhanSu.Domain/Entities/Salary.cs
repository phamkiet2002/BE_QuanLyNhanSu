using QuanLyNhanSu.Domain.Abstractions.Entities;
using QuanLyNhanSu.Domain.Enumerations;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class Salary : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public decimal Salarys { get; set; }
    public Status? Status { get; set; }
    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public DateTime? EndDate { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion

    public Salary(Guid id, decimal salarys, Guid? employeeId)
    {
        Id = id;
        Salarys = salarys;
        EmployeeId = employeeId;
        Status = StatusEnums.Status.Active;
        CreatedDate = DateTime.Now;
    }

    public static Salary CreateSalary(Guid id, decimal salarys, Guid? employeeId)
    {
        return new Salary(id, salarys, employeeId);
    }

    public void UpdateSalary(Status? status, DateTime? endDate)
    {
        Status = status;
        EndDate = endDate;
    }
}
