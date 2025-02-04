using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class AttendanceController : ApiController
{
    public AttendanceController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost("checkinandcheckout")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckInCheckOutAttendance([FromBody] Contract.Service.V1.Attendance.Command.CreateCheckInCheckOutAttendanceCommand command)
    {
        var result = await Sender.Send(command);
        return Ok(result);
    }

    [HttpPost("checkin")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER,EMPLOYEE")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckInAttendance([FromBody] Contract.Service.V1.Attendance.Command.CheckInAttendanceCommand command)
    {
        var result = await Sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER")]
    [ProducesResponseType(typeof(Result<IEnumerable<Contract.Service.V1.Employee.Response.EmployeeMapToAttendanceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAttedance(string? WorkPlaceName = null, DateTime? Month = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Contract.Service.V1.Employee.Query.GetEmployeesMapToAttendanceQuery(WorkPlaceName, Month, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpGet("getemployeeId")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,EMPLOYEE")]
    [ProducesResponseType(typeof(Result<Contract.Service.V1.Employee.Response.EmployeeMapToAttendanceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmployeeByIdAttendace()
    {
        var result = await Sender.Send(new Contract.Service.V1.Employee.Query.GetEmployeeByIdAttendaceQuery());
        return Ok(result);
    }

    [HttpPut("checkout")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER,EMPLOYEE")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckOutAttendance([FromBody] Contract.Service.V1.Attendance.Command.CheckOutAttendanceCommand command)
    {
        var result = await Sender.Send(command);
        return Ok(result);
    }

    [HttpPut("overtimeOutsideWorkHours/{attendanceId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckOvertimeOutsideWorkHoursAttendance(Guid attendanceId, [FromBody] Contract.Service.V1.Attendance.Command.OvertimeOutsideWorkHourAttendanceCommand command)
    {
        var updateAttendanceCommand = new Contract.Service.V1.Attendance.Command.OvertimeOutsideWorkHourAttendanceCommand
        (
            attendanceId, command.StartTime, command.EndTime
        );
        var result = await Sender.Send(updateAttendanceCommand);
        return Ok(result);
    }

    [HttpPut("updateattendance/{attendanceId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAttendacne(Guid attendanceId, [FromBody] Contract.Service.V1.Attendance.Command.UpdateAttendanceCommand command)
    {
        var updateAttendanceCommand = new Contract.Service.V1.Attendance.Command.UpdateAttendanceCommand
        (
            attendanceId, command.CheckIn, command.CheckOut, command.ReasonNote
        );
        var result = await Sender.Send(updateAttendanceCommand);
        return Ok(result);
    }

    [HttpPut("updateAbsent/{attendanceId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateIsAbsentAttendance(Guid attendanceId, [FromBody] Contract.Service.V1.Attendance.Command.UpdateIsAbsentAttendanceCommand command)
    {
        var updateIsAbsentAttendanceCommand = new Contract.Service.V1.Attendance.Command.UpdateIsAbsentAttendanceCommand
        (
            attendanceId, command.IsAbsent
        );
        var result = await Sender.Send(updateIsAbsentAttendanceCommand);
        return Ok(result);
    }

    //[HttpPost("isabsent")]
    //[Authorize(Roles = "ADMIN,HR_MANAGER")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateStandardWorkingDay([FromBody] Command.CheckIsAbsentAttendanceCommand createStandardWorking)
    //{
    //    var result = await Sender.Send(createStandardWorking);
    //    return Ok(result);
    //}
}
