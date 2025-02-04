using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WorkPlace;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;

[Authorize]
public class WorkPlaceController : ApiController
{
    public WorkPlaceController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateWorkPlace([FromBody] Command.CreateWorkPlaceCommand createWorkPlace)
    {
        var result = await Sender.Send(createWorkPlace);
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,DEPARTMENT_MANAGER,HR_MANAGER")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.WorkPlaceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllWorkPlace(string SearchTerm = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetWorkPlacesQuery(SearchTerm, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpGet("getid/{workPlaceId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(typeof(Result<Response.WorkPlaceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetWorkPlaceById(Guid workPlaceId)
    {
        var result = await Sender.Send(new Query.GetWorkPlaceByIdQuery(workPlaceId));
        return Ok(result);
    }

    [HttpPut("{workPlaceId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateWorkPlace(Guid workPlaceId, Command.UpdateWorkPlaceCommand updateWorkPlace)
    {
        var updateWorkPlaceCommand = new Command.UpdateWorkPlaceCommand(workPlaceId, updateWorkPlace.Name, updateWorkPlace.Phone, updateWorkPlace.Email, updateWorkPlace.Address);
        var result = await Sender.Send(updateWorkPlaceCommand);
        return Ok(result);
    }

    [HttpDelete("{workPlaceId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteWorkPlace(Guid workPlaceId)
    {
        var result = await Sender.Send(new Command.DeleteWorkPlaceCommand(workPlaceId));
        return Ok(result);
    }
}
