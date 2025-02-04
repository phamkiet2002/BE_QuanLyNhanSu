using QuanLyNhanSu.Domain.Abstractions.Entities;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;
using static QuanLyNhanSu.Domain.Enumerations.LeaveRegistrationEnums;

namespace QuanLyNhanSu.Domain.Entities;

public class LeaveRegistration : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    // chọn nghỉ ngày hoặc nghỉ buổi
    public TypeOfLeave TypeOfLeave { get; set; }
    //nghi ngay
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    //nghi buoi
    public HalfDayOff? HalfDayOff { get; set; }
    public DateTime? DayOff { get; set; }
    public string LeaveReason { get; set; }
    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }

    public Guid? LeaveDateId { get; set; }
    public virtual LeaveDate LeaveDate { get; set; }

    public DateTime? DateCancel { get; set; }
    #endregion

    #region =====Approve=====
    // Approve
    public PendingApproval? PendingApproval { get; set; }
    public DateTime? ApprovedTime { get; set; }
    public Guid? ApprovedId { get; set; }
    public virtual Employee Approval { get; set; }
    public string? ApprovalNote { get; set; }
    #endregion

    #region =====audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion


    public LeaveRegistration()
    {
    }

    // create cho nghỉ ngày
    public LeaveRegistration(Guid id, DateTime? startDate, DateTime? endDate, string leaveReason, Guid? employeeId, Guid? leaveDateId)
    {
        Id = id;
        TypeOfLeave = TypeOfLeave.Nghingay;
        StartDate = startDate;
        EndDate = endDate;
        LeaveReason = leaveReason;
        EmployeeId = employeeId;
        LeaveDateId = leaveDateId;
        PendingApproval = Enumerations.ApproveEmuns.PendingApproval.Chuaduyet;
        CreatedDate = DateTime.Now;
    }

    public static LeaveRegistration CreateLeaveRegistrationTypeOfLeaveNghiNgay(Guid id, DateTime? startDate, DateTime? endDate, string leaveReason, Guid? employeeId, Guid? leaveDateId)
    {
        return new LeaveRegistration(id, startDate, endDate, leaveReason, employeeId, leaveDateId);
    }

    // create cho nghỉ buổi
    public LeaveRegistration(Guid id, HalfDayOff? halfDayOff, DateTime dayoff, string leaveReason, Guid? employeeId, Guid? leaveDateId)
    {
        Id = id;
        TypeOfLeave = TypeOfLeave.Nghibuoi;
        HalfDayOff = halfDayOff;
        DayOff = dayoff;
        LeaveReason = leaveReason;
        EmployeeId = employeeId;
        LeaveDateId = leaveDateId;
        PendingApproval = Enumerations.ApproveEmuns.PendingApproval.Chuaduyet;
        CreatedDate = DateTime.Now;
    }

    public static LeaveRegistration CreateLeaveRegistrationTypeOfLeaveNghiBuoi(Guid id, HalfDayOff? halfDayOff, DateTime? dayOff, string leaveReason, Guid? employeeId, Guid? leaveDateId)
    {
        return new LeaveRegistration(id, halfDayOff, dayOff.Value, leaveReason, employeeId, leaveDateId);
    }

    public void CancelLeaveRegistration()
    {
        PendingApproval = Enumerations.ApproveEmuns.PendingApproval.Dahuy;
        DateCancel = DateTime.Now;
    }

    public void ApproveLeaveRegistration(Guid approvedId, string approvalNote, PendingApproval? pendingApproval)
    {
        PendingApproval = pendingApproval;
        ApprovalNote = approvalNote;
        ApprovedTime = DateTime.Now;
        ApprovedId = approvedId;
    }
}
