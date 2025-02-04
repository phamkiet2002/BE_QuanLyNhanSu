using QuanLyNhanSu.Domain.Abstractions.Entities;
using QuanLyNhanSu.Domain.Enumerations;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class EmployeeLevel : DomainEntity<Guid>
{
    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }

    public Guid? LevelId { get; set; }
    public virtual Level Level { get; set; }

    public Status? Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? EndDate { get; set; }

    public EmployeeLevel() { }

    public EmployeeLevel(Guid? employeeId, Guid? levelId)
    {
        EmployeeId = employeeId;
        LevelId = levelId;
        Status = StatusEnums.Status.Active;
        CreatedDate = DateTime.Now;
    }

    public static EmployeeLevel CreateEmployeeLevel(Guid? employeeId, Guid? levelId)
    {
        return new EmployeeLevel(employeeId, levelId);
    }

    public void UpdateEmployeeLevel(Status status, DateTime? endate)
    {
        Status = status;
        EndDate = endate;
    }
}
