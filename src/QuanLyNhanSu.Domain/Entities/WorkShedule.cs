using QuanLyNhanSu.Domain.Abstractions.Entities;

namespace QuanLyNhanSu.Domain.Entities;

public class WorkShedule : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public string Name { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public TimeSpan BreakStartTime { get; private set; }
    public TimeSpan BreakEndTime { get; private set; }
    public bool? IsDeleted { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion
    public virtual ICollection<Department> Departments { get; set; }

    protected WorkShedule()
    {
        Departments = new List<Department>();
    }
    public WorkShedule(Guid id, string name, TimeSpan startTime, TimeSpan endTime, TimeSpan breakStartTime, TimeSpan breakEndTime)
    {
        Id = id;
        Name = name;
        StartTime = startTime;
        EndTime = endTime;
        BreakStartTime = breakStartTime;
        BreakEndTime = breakEndTime;
        CreatedDate = DateTime.Now;
    }

    public static WorkShedule CreateWorkShedule(string name, TimeSpan startTime, TimeSpan endTime, TimeSpan breakStartTime, TimeSpan breakEndTime)
    {
        return new WorkShedule(Guid.NewGuid(), name, startTime, endTime, breakStartTime, breakEndTime);
    }

    public void UpdateWorkShedule(string name, TimeSpan startTime, TimeSpan endTime, TimeSpan breakStartTime, TimeSpan breakEndTime)
    {
        Name = name;
        StartTime = startTime;
        EndTime = endTime;
        BreakStartTime = breakStartTime;
        BreakEndTime = breakEndTime;
    }

    public void DeleteWorkShedule()
    {
        IsDeleted = true;
        DeletedDate = DateTime.Now;
    }
}
