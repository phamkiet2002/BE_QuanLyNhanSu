using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Service.V1.AttendanceSetting;
using QuanLyNhanSu.Domain.Enumerations;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class AttendanceSettingController : ApiController
{
    public AttendanceSettingController(ISender _sender) : base(_sender)
    {
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAttendanceSetting(StatusEnums.Status? Status = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetAttendanceSettingQuery(Status, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAttendanceSetting([FromBody] Command.CreateAttendanceSettingCommand createAttendanceSetting)
    {
        var result = await Sender.Send(createAttendanceSetting);
        return Ok(result);
    }

    [HttpPut("{attendanceSettingId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAttendanceSetting(Guid attendanceSettingId, [FromBody] Command.UpdateAttendanceSettingCommand updateAttendanceSetting)
    {
        var updateAttendanceSettingCommand = new Command.UpdateAttendanceSettingCommand
        (
            attendanceSettingId, updateAttendanceSetting.MaximumLateAllowed, updateAttendanceSetting.MaxEarlyLeaveAllowed
        );
        var result = await Sender.Send(updateAttendanceSettingCommand);
        return Ok(result);
    }

    [HttpDelete("{attendanceSettingId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAttendanceSetting(Guid attendanceSettingId)
    {
        var deleteAttendanceSettingCommand = new Command.DeleteAttendanceSettingCommand(attendanceSettingId);
        var result = await Sender.Send(deleteAttendanceSettingCommand);
        return Ok(result);
    }
}
