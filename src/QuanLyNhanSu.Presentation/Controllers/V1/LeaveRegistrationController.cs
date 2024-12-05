using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;
using QuanLyNhanSu.Presentation.Abstractions;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;

namespace QuanLyNhanSu.Presentation.Controllers.V1;

public class LeaveRegistrationController : ApiController
{
    public LeaveRegistrationController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost("dayoff")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLeaveRegistrationNghiNgay([FromBody] Command.CreateLeaveRegistrationNghiNgayCommand command)
    {
        var result = await Sender.Send(command);
        return Ok(result);
    }

    [HttpPost("halfdayoff")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLeaveRegistrationNghiBuoi([FromBody] Command.CreateLeaveRegistrationNghiBuoiCommand command)
    {
        var result = await Sender.Send(command);
        return Ok(result);
    }

    [HttpGet("dayoff")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.LeaveRegistrationTypeOfLeaveDayOffResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllLeaveRegistrationTypeOfLeaveDayOff(PendingApproval PendingApproval, string SearchTerm = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetLeaveRegistrationTypeOfLeaveDayOffsQuery(SearchTerm, PendingApproval, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpGet("halfdayoff")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.LeaveRegistrationTypeOfLeaveHalfDayOffResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllLeaveRegistrationTypeOfHalfDayOff(PendingApproval PendingApproval, string SearchTerm = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetLeaveRegistrationTypeOfLeaveHalfDayOffsQuery(SearchTerm, PendingApproval, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpPut("cancelleaveregistration/{leaveRegistrationId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelLeaveRegistration(Guid leaveRegistrationId)
    {
        var result = await Sender.Send(new Command.CancelLeaveRegistrationCommand(leaveRegistrationId));
        return Ok(result);
    }
}
