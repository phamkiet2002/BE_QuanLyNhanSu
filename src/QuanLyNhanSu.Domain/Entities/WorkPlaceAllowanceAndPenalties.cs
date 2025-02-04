using QuanLyNhanSu.Domain.Abstractions.Entities;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class WorkPlaceAllowanceAndPenalties : DomainEntity<Guid>
{
    public Guid? WorkPlaceId { get; set; }
    public virtual WorkPlace WorkPlace { get; set; }

    public Guid? AllowanceAndPenaltiesId { get; set; }
    public virtual AllowanceAndPenalties AllowanceAndPenalties { get; set; }

    public Status? Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? EndDate { get; set; }

    public WorkPlaceAllowanceAndPenalties() { }

    public WorkPlaceAllowanceAndPenalties(Guid id, Guid? workPlaceId, Guid? allowanceAndPenaltiesId)
    {
        Id = id;
        WorkPlaceId = workPlaceId;
        AllowanceAndPenaltiesId = allowanceAndPenaltiesId;
        Status = Enumerations.StatusEnums.Status.Active;
        CreatedDate = DateTime.Now;
        EndDate = null;
    }

    public static WorkPlaceAllowanceAndPenalties CreateWorkPlaceAllowanceAndPenalties(Guid id, Guid? workPlaceId, Guid? allowanceAndPenaltiesId)
    {
        return new WorkPlaceAllowanceAndPenalties(id, workPlaceId, allowanceAndPenaltiesId);
    }

    public void UpdateWorkPlaceAllowanceAndPenalties(Status? status, DateTime? endDate)
    {
        Status = status;
        EndDate = endDate;
    }
}
