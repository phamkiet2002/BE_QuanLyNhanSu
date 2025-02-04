using QuanLyNhanSu.Domain.Abstractions.Entities;
using QuanLyNhanSu.Domain.Enumerations;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class EmployeePosition : DomainEntity<Guid>
{
    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }

    public Guid? PositionId { get; set; }
    public virtual Position Position { get; set; }

    public Status? Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? EndDate { get; set; }

    public EmployeePosition() { }

    public EmployeePosition(Guid? employeeId, Guid? positionId)
    {
        EmployeeId = employeeId;
        PositionId = positionId;
        Status = StatusEnums.Status.Active;
        CreatedDate = DateTime.Now;
    }

    public static EmployeePosition CreateEmployeePosition(Guid? employeeId, Guid? positionId)
    {
        return new EmployeePosition(employeeId, positionId);
    }

    public void UpdateEmployeePosition(Status status, DateTime? endDate)
    {
        Status = status;
        EndDate = endDate;
    }
}
