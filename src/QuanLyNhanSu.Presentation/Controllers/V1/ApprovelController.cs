using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;

[Authorize]
public class ApprovelController : ApiController
{
    public ApprovelController(ISender _sender) : base(_sender)
    {
    }

    [HttpPut("leave-registration/approvel/{leaveRegistrationId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ApprovelLeaveRegistration(Guid leaveRegistrationId, [FromBody] Command.ApproveLeaveRegistrationCommand approveLeaveRegistration)
    {
        var updateLeaveRegistrationCommand = new Command.ApproveLeaveRegistrationCommand
        (
            leaveRegistrationId, approveLeaveRegistration.ApprovalNote, approveLeaveRegistration.PendingApproval
        );
        var result = await Sender.Send(updateLeaveRegistrationCommand);
        return Ok(result);
    }

    [HttpPut("attendance/approvel/{attendanceId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ApprovelAttendance(Guid attendanceId, [FromBody] Contract.Service.V1.Attendance.Command.ApproveAttendanceCommand approveAttendace)
    {
        var updateAttendanceCommand = new Contract.Service.V1.Attendance.Command.ApproveAttendanceCommand
        (
            attendanceId, approveAttendace.PendingApproval, approveAttendace.ApprovalNote, approveAttendace.IsLate, approveAttendace.IsEarlyLeave
        );
        var result = await Sender.Send(updateAttendanceCommand);
        return Ok(result);
    }
}
