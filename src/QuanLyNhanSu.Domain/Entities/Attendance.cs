using QuanLyNhanSu.Domain.Abstractions.Entities;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;

namespace QuanLyNhanSu.Domain.Entities;

public class Attendance : DomainEntity<Guid>, IAuditEntity
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public bool? IsLate { get; set; }
    public DateTime? LateTime { get; set; } //=> thời gian đi trễ
    public bool IsEarlyLeave { get; set; }
    public DateTime? EarlyLeaveTime { get; set; } //=> thời gian về sớm

    // quản lý phòng ban xem xét và ghi nhận vào hệ thống đối với trường hợp nhân viên ra ngoài quá lâu
    public bool OvertimeOutsideWorkHours { get; set; }
    // thời gian nhân viên ra khỏi nơi làm việc quá
    // lâu
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    // Approve
    public PendingApproval? PendingApproval { get; set; }
    public DateTime? ApprovedTime { get; set; }
    public Guid? ApprovedId { get; set; }
    public virtual Employee Approval { get; set; }

    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }


    public Guid? EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public Attendance()
    {

    }

    public Attendance(Guid id, bool isLate, DateTime? lateTime, Guid? employeeId)
    {
        Id = id;
        CheckIn = DateTime.Now;
        IsLate = isLate;
        LateTime = lateTime;
        EmployeeId = employeeId;
        CreatedDate = DateTime.Now;
    }

    public static Attendance CheckInAttendance(Guid id, bool isLate, DateTime? lateTime, Guid? employeeId)
    {
        return new Attendance(id, isLate, lateTime, employeeId);
    }

    public void CheckOutAttendance(bool isEarlyLeave, DateTime? earlyLeaveTime, Guid? employeeId)
    {
        CheckOut = DateTime.Now;
        IsEarlyLeave = isEarlyLeave;
        EarlyLeaveTime = earlyLeaveTime;
        EmployeeId = employeeId;
    }
}
