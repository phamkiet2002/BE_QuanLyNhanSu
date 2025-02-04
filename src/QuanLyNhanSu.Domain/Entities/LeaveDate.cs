using QuanLyNhanSu.Domain.Abstractions.Entities;

namespace QuanLyNhanSu.Domain.Entities;

public class LeaveDate : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public string Name { get; private set; }
    public int TotalAnnualLeaveDate { get; private set; }
    public int MaximumDaysOffPerMonth { get; private set; }
    public string Description { get; private set; }
    public bool IsHoliday { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsDeleted { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion

    public virtual ICollection<LeaveRegistration> LeaveRegistrations { get; set; }

    public LeaveDate()
    {
        
    }

    public LeaveDate(Guid id, string name, int totalAnnualLeaveDate, int maximumDaysOffPerMonth, string description, bool isHoliday, DateTime? startDate, DateTime? endDate)
    {
        Id = id;
        Name = name;
        TotalAnnualLeaveDate = totalAnnualLeaveDate;
        MaximumDaysOffPerMonth = maximumDaysOffPerMonth;
        Description = description;
        CreatedDate = DateTime.Now;
        IsHoliday = isHoliday;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static LeaveDate CreateLeaveDate(Guid id, string name, int totalAnnualLeaveDate, int maximumDaysOffPerMonth, string description, bool isHoliday, DateTime? startDate, DateTime? endDate)
    {
        return new LeaveDate(id, name, totalAnnualLeaveDate, maximumDaysOffPerMonth, description, isHoliday, startDate, endDate);
    }

    public void UpdateLeaveDate(string name, int totalAnnualLeaveDate, int maximumDaysOffPerMonth, string description, bool isHoliday, DateTime? startDate, DateTime? endDate)
    {
        Name = name;
        TotalAnnualLeaveDate = totalAnnualLeaveDate;
        MaximumDaysOffPerMonth = maximumDaysOffPerMonth;
        Description = description;
        IsHoliday = isHoliday;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void DeleteLeaveDate()
    {
        IsDeleted = true;
        DeletedDate = DateTime.Now;
    }
}
