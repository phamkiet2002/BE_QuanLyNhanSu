using QuanLyNhanSu.Domain.Abstractions.Entities;
using QuanLyNhanSu.Domain.Enumerations;
using static QuanLyNhanSu.Domain.Enumerations.StatusEnums;

namespace QuanLyNhanSu.Domain.Entities;
public class AttendanceSetting : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public TimeSpan MaximumLateAllowed { get; set; }
    public TimeSpan MaxEarlyLeaveAllowed { get; set; }
    public Status? Status { get; private set; }
    public bool? IsDeleted { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion

    public AttendanceSetting()
    {
    }

    public AttendanceSetting(Guid id, TimeSpan maximumLateAllowed, TimeSpan maxEarlyLeaveAllowed)
    {
        Id = id;
        MaximumLateAllowed = maximumLateAllowed;
        MaxEarlyLeaveAllowed = maxEarlyLeaveAllowed;
        Status = StatusEnums.Status.Active;
        CreatedDate = DateTime.Now;
    }

    public static AttendanceSetting CreateAttendanceSetting(Guid id, TimeSpan maximumLateAllowed, TimeSpan maxEarlyLeaveAllowed)
    {
        return new AttendanceSetting(id, maximumLateAllowed, maxEarlyLeaveAllowed);
    }

    public static AttendanceSetting UpdateStatusAttendanceSetting(Guid id, Status status)
    {
        return new AttendanceSetting()
        {
            Id = id,
            Status = status
        };
    }

    public void UpdateAttendanceSetting(TimeSpan maximumLateAllowed, TimeSpan maxEarlyLeaveAllowed)
    {
        MaximumLateAllowed = maximumLateAllowed;
        MaxEarlyLeaveAllowed = maxEarlyLeaveAllowed;
    }

    public void UpdateStatus(Status status)
    {
        Status = status;
        IsDeleted = true;
        DeletedDate = DateTime.Now;
    }
}

