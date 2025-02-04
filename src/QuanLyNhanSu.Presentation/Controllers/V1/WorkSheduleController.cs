using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Presentation.Abstractions;
using QuanLyNhanSu.Contract.Service.V1.WorkShedule;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class WorkSheduleController : ApiController
{
    public WorkSheduleController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateWorkShedule([FromBody] Command.CreateWorkSheduleCommand createWorkShedule)
    {
        var result = await Sender.Send(createWorkShedule);
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.WorkSheduleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllWorkShedule(string? SearchTerm = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetWorkShedulesQuery(SearchTerm, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpPut("{worSheduleId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateWorkShedule(Guid worSheduleId, [FromBody] Command.UpdateWorkSheduleCommand updateWorkShedule)
    {
        var updateWorkSheduleCommand = new Command.UpdateWorkSheduleCommand(worSheduleId, updateWorkShedule.Name, updateWorkShedule.StartTime, updateWorkShedule.EndTime, updateWorkShedule.BreakStartTime, updateWorkShedule.BreakEndTime);
        var result = await Sender.Send(updateWorkSheduleCommand);
        return Ok(result);
    }

    [HttpDelete("{worSheduleId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteWorkShedule(Guid worSheduleId)
    {
        var result = await Sender.Send(new Command.DeleteWorkSheduleCommand(worSheduleId));
        return Ok(result);
    }
}
