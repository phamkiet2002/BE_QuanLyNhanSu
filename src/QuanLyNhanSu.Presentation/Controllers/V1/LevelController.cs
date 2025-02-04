using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Level;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class LevelController : ApiController
{
    public LevelController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLevel([FromBody] Command.CreateLevelCommand createLevel)
    {
        var result = await Sender.Send(createLevel);
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,DEPARTMENT_MANAGER,HR_MANAGER")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.LevelResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllLevel(string SearchTerm = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetLevelsQuery(SearchTerm, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpPut("{levelId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateLevel(Guid levelId, [FromBody] Command.UpdateLevelCommand updateLevel)
    {
        var updateLevelCommand = new Command.UpdateLevelCommand(levelId, updateLevel.Name, updateLevel.Description);
        var result = await Sender.Send(updateLevelCommand);
        return Ok(result);
    }

    [HttpDelete("{levelId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteLevel(Guid levelId)
    {
        var result = await Sender.Send(new Command.DeleteLevelCommand(levelId));
        return Ok(result);
    }
}
