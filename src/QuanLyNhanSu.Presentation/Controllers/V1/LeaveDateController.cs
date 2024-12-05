using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveDate;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;

public class LeaveDateController : ApiController
{
    public LeaveDateController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLeaveDate([FromBody] Command.CreateLeaveDateCommand createLeaveDate)
    {
        var result = await Sender.Send(createLeaveDate);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.LeaveDateResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllLeaveDate(string SearchTerm = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetLeaveDatesQuery(SearchTerm, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpPut("{leaveDateId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateLeaveDate(Guid leaveDateId, [FromBody] Command.UpdateLeaveDateCommand updateLeaveDate)
    {
        var updateLeaveDateCommand = new Command.UpdateLeaveDateCommand
        (
            leaveDateId, updateLeaveDate.Name, updateLeaveDate.TotalAnnualLeaveDate, 
            updateLeaveDate.MaximumDaysOffPerMonth, updateLeaveDate.Description
        );  
        var result = await Sender.Send(updateLeaveDateCommand);
        return Ok(result);
    }

    [HttpDelete("{leaveDateId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteLeaveDate(Guid leaveDateId)
    {
        var result = await Sender.Send(new Command.DeleteLeaveDateCommand(leaveDateId));
        return Ok(result);
    }
}
