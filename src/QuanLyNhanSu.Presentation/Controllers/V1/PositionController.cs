using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Position;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class PositionController : ApiController
{
    public PositionController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePosition([FromBody] Command.CreatePositionCommand createPosition)
    {
        var result = await Sender.Send(createPosition);
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,DEPARTMENT_MANAGER,HR_MANAGER")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.PositionResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllPosition(string SearchTerm = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetPositionsQuery(SearchTerm, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpPut("{positionId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePosition(Guid positionId, [FromBody] Command.UpdatePositionCommand updatePosition)
    {
        var updatePositionCommand = new Command.UpdatePositionCommand(positionId, updatePosition.Name, updatePosition.Description, updatePosition.RoleIds);
        var result = await Sender.Send(updatePositionCommand);
        return Ok(result);
    }

    [HttpDelete("{positionId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePosition(Guid positionId)
    {
        var result = await Sender.Send(new Command.DeletePositionCommand(positionId));
        return Ok(result);
    }
}
