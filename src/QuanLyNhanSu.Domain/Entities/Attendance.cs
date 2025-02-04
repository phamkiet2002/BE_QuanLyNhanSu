using System.ComponentModel;
using System;
using System.Xml.Serialization;
using QuanLyNhanSu.Domain.Abstractions.Entities;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;
using static QuanLyNhanSu.Domain.Enumerations.AttendanceEmuns;

namespace QuanLyNhanSu.Domain.Entities;

public class Attendance : DomainEntity<Guid>, IAuditEntity
{
    #region =====attribute=====
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public bool? IsLate { get; set; }
    public DateTime? LateTime { get; set; } //=> thời gian đi trễ
    public bool? IsEarlyLeave { get; set; }
    public DateTime? EarlyLeaveTime { get; set; } //=> thời gian về sớm

    // quản lý phòng ban xem xét và ghi nhận vào hệ thống đối với trường hợp nhân viên ra ngoài quá lâu
    // thời gian nhân viên ra khỏi nơi làm việc quá lâu
    public bool? OvertimeOutsideWorkHours { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool? IsAbsent { get; set; }
    public bool? LeaveRequest { get; set; }
    #endregion

    #region ==============Approve================
    // Approve
    public PendingApproval? PendingApproval { get; set; }
    public DateTime? ApprovedTime { get; set; }
    public Guid? ApprovedId { get; set; }
    public virtual Employee Approval { get; set; }
    public string? ApprovalNote { get; set; }
    public string? ReasonNote { get; set; }
    #endregion

    #region =====Audit=====
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    #endregion
    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public Attendance()
    {

    }

    public Attendance(Guid id, bool? isLate, DateTime? lateTime, Guid? employeeId)
    {
        Id = id;
        CheckIn = DateTime.Now;
        IsLate = isLate;
        LateTime = lateTime;
        PendingApproval = Enumerations.ApproveEmuns.PendingApproval.Chuaduyet;
        EmployeeId = employeeId;
        CreatedDate = DateTime.Now;
    }

    public static Attendance CheckInAttendance(Guid id, bool? isLate, DateTime? lateTime, Guid? employeeId)
    {
        return new Attendance(id, isLate, lateTime, employeeId);
    }

    public void CheckOutAttendance(bool? isEarlyLeave, DateTime? earlyLeaveTime, Guid? employeeId)
    {
        CheckOut = DateTime.Now;
        IsEarlyLeave = isEarlyLeave;
        EarlyLeaveTime = earlyLeaveTime;
        PendingApproval = Enumerations.ApproveEmuns.PendingApproval.Chuaduyet;
        EmployeeId = employeeId;
    }

    public Attendance(Guid id, Guid? employeeId)
    {
        Id = id;
        IsAbsent = true;
        EmployeeId = employeeId;
        CreatedDate = DateTime.Now;
    }

    public static Attendance CheckAbsent(Guid id, Guid? employeeId)
    {
        return new Attendance(id, employeeId);
    }

    public Attendance
    (
        Guid id, DateTime? checkIn, DateTime? checkOut,
        bool? isLate, bool? isEarlyLeave, bool? overtimeOutsideWorkHours, bool? isAbsent, bool? leaveRequest,
        string? approvalNote, Guid? employeeId)
    {
        Id = id;
        CheckIn = checkIn;
        CheckOut = checkOut;
        IsLate = isLate;
        IsEarlyLeave = isEarlyLeave;
        OvertimeOutsideWorkHours = overtimeOutsideWorkHours;
        IsAbsent = isAbsent;
        LeaveRequest = leaveRequest;
        ApprovalNote = approvalNote;
        EmployeeId = employeeId;
        CreatedDate = DateTime.Now;
    }

    public static Attendance CreateCheckInCheckOutAttendance
    (
        Guid id, DateTime? checkIn, DateTime? checkOut,
        bool? isLate, bool? isEarlyLeave, bool? overtimeOutsideWorkHours, bool? isAbsent, bool? leaveRequest,
        string? approvalNote, Guid? employeeId)
    {
        return new Attendance(id, checkIn, checkOut, isLate, isEarlyLeave, overtimeOutsideWorkHours, isAbsent, leaveRequest, approvalNote, employeeId);
    }

    public void ApproveAttendance(PendingApproval? pendingApproval, Guid? approvedId, string approvalNote, bool isLate, bool isEarlyLeave)
    {
        PendingApproval = pendingApproval;
        ApprovedTime = DateTime.Now;
        ApprovedId = approvedId;
        ApprovalNote = approvalNote;
        IsLate = isLate;
        IsEarlyLeave = isEarlyLeave;
    }

    public void OvertimeOutsideWorkHoursCheckAttendance(DateTime? startTime, DateTime? endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
        OvertimeOutsideWorkHours = true;
        PendingApproval = Enumerations.ApproveEmuns.PendingApproval.Chuaduyet;
    }

    public void UpdateAttendance(DateTime? checkIn, DateTime? checkOut, string reasonNote)
    {
        CheckIn = checkIn;
        CheckOut = checkOut;
        ReasonNote = reasonNote;
        UpdatedDate = DateTime.Now;
    }

    // sử dụng cho trường hợp nghỉ có lý do chính đáng
    public void UpdateIsAbsentAttendance(bool isAbsent)
    {
        IsAbsent = isAbsent;
    }
}
