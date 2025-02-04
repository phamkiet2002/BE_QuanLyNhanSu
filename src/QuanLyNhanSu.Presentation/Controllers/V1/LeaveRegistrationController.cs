using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;
using QuanLyNhanSu.Presentation.Abstractions;
using static QuanLyNhanSu.Domain.Enumerations.ApproveEmuns;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class LeaveRegistrationController : ApiController
{
    public LeaveRegistrationController(ISender _sender) : base(_sender)
    {
    }

    [HttpGet("employeLeaveRegistration")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER,EMPLOYEE")]
    [ProducesResponseType(typeof(Result<IEnumerable<Contract.Service.V1.Employee.Response.EmployeeByIdLeaveRegistrationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmployeeLeaveRegistration(DateTime? Month = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Contract.Service.V1.Employee.Query.GetEmployeeByIdLeaveRegistrationQuery(Month, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpPost("dayoff")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER,EMPLOYEE")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLeaveRegistrationNghiNgay([FromBody] Command.CreateLeaveRegistrationNghiNgayCommand command)
    {
        var result = await Sender.Send(command);
        return Ok(result);
    }

    [HttpPost("halfdayoff")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER,EMPLOYEE")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLeaveRegistrationNghiBuoi([FromBody] Command.CreateLeaveRegistrationNghiBuoiCommand command)
    {
        var result = await Sender.Send(command);
        return Ok(result);
    }

    [HttpGet("dayoff")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER,EMPLOYEE")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.LeaveRegistrationTypeOfLeaveDayOffResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllLeaveRegistrationTypeOfLeaveDayOff(PendingApproval PendingApproval, string SearchTerm = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetLeaveRegistrationTypeOfLeaveDayOffsQuery(SearchTerm, PendingApproval, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpGet("halfdayoff")]
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER,EMPLOYEE")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.LeaveRegistrationTypeOfLeaveHalfDayOffResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllLeaveRegistrationTypeOfHalfDayOff(PendingApproval PendingApproval, string SearchTerm = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetLeaveRegistrationTypeOfLeaveHalfDayOffsQuery(SearchTerm, PendingApproval, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }
    [Authorize(Roles = "ADMIN,HR_MANAGER,DEPARTMENT_MANAGER,EMPLOYEE")]
    [HttpPut("cancelleaveregistration/{leaveRegistrationId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelLeaveRegistration(Guid leaveRegistrationId)
    {
        var result = await Sender.Send(new Command.CancelLeaveRegistrationCommand(leaveRegistrationId));
        return Ok(result);
    }
}
