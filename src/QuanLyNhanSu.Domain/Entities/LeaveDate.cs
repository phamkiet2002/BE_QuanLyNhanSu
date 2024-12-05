using QuanLyNhanSu.Domain.Abstractions.Entities;

namespace QuanLyNhanSu.Domain.Entities;

public class LeaveDate : DomainEntity<Guid>, IAuditEntity
{
    public string Name { get; private set; }
    public int TotalAnnualLeaveDate { get; private set; }
    public int MaximumDaysOffPerMonth { get; private set; }
    public string Description { get; private set; }

    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<LeaveRegistration> LeaveRegistrations { get; set; }

    public LeaveDate(Guid id, string name, int totalAnnualLeaveDate, int maximumDaysOffPerMonth, string description)
    {
        Id = id;
        Name = name;
        TotalAnnualLeaveDate = totalAnnualLeaveDate;
        MaximumDaysOffPerMonth = maximumDaysOffPerMonth;
        Description = description;
        CreatedDate = DateTime.Now;
    }

    public static LeaveDate CreateLeaveDate(Guid id, string name, int totalAnnualLeaveDate, int maximumDaysOffPerMonth, string description)
    {
        return new LeaveDate(id, name, totalAnnualLeaveDate, maximumDaysOffPerMonth, description);
    }

    public void UpdateLeaveDate(string name, int totalAnnualLeaveDate, int maximumDaysOffPerMonth, string description)
    {
        Name = name;
        TotalAnnualLeaveDate = totalAnnualLeaveDate;
        MaximumDaysOffPerMonth = maximumDaysOffPerMonth;
        Description = description;
    }

    public void DeleteLeaveDate()
    {
        IsDeleted = true;
        DeletedDate = DateTime.Now;
    }
}
