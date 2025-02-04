using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.AllowanceAndPenalties;
using QuanLyNhanSu.Domain.Enumerations;
using QuanLyNhanSu.Presentation.Abstractions;
using static QuanLyNhanSu.Domain.Enumerations.AllowanceAndPenaltyEnums;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class AllowanceAndPenaltiesController : ApiController
{
    public AllowanceAndPenaltiesController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost("allowance")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAllowance([FromBody] Command.CreateAllowanceCommand createAllowance)
    {
        var result = await Sender.Send(createAllowance);
        return Ok(result);
    }

    [HttpPost("penalties")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePenalties([FromBody] Command.CreatePenaltiesCommand createPenalties)
    {
        var result = await Sender.Send(createPenalties);
        return Ok(result);
    }

    [HttpGet("allowance")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.AllowanceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAllowance(TypeOfAllowance? TypeOfAllowance = null, string? WorkPlaceName = null, string? SortOder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetAllowancesQuery(TypeOfAllowance, WorkPlaceName, SortOder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpGet("penalties")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.PenaltiesResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllPenalties(TypeOfPenalty? TypeOfPenalty = null, string? WorkPlaceName = null, string? SortOder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetAllPenaltiesQuery(TypeOfPenalty, WorkPlaceName, SortOder, PageIndex, PageSize));
        return Ok(result);
    }
    [HttpPut("allowance/{allowanId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAllowance(Guid allowanId, [FromBody] Command.UpdateAllowanceCommand updateAllowance)
    {
        var updateAllowanceCommand = new Command.UpdateAllowanceCommand
        (
            allowanId, updateAllowance.TypeOfAllowance, updateAllowance.Money,
            updateAllowance.EffectiveDate, updateAllowance.Note, updateAllowance.IsAllWorkPlace, updateAllowance.WorkPlaceId
        );
        var result = await Sender.Send(updateAllowanceCommand);
        return Ok(result);
    }

    [HttpPut("penalties/{penaltiesId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePenalty(Guid penaltiesId, [FromBody] Command.UpdatePenaltiesCommand updatePenalties)
    {
        var updatePenaltiesCommand = new Command.UpdatePenaltiesCommand
        (
            penaltiesId, updatePenalties.TypeOfPenalty, updatePenalties.Money,
            updatePenalties.EffectiveDate, updatePenalties.Note, updatePenalties.IsAllWorkPlace, updatePenalties.WorkPlaceId
        );
        var result = await Sender.Send(updatePenaltiesCommand);
        return Ok(result);
    }

    [HttpDelete("allowanceAndPenalties/{allowanceAndPenaltiesId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAllowanceAndPenalties(Guid allowanceAndPenaltiesId)
    {
        var result = await Sender.Send(new Command.DeleteAllowanceAndPenaltiesCommand(allowanceAndPenaltiesId));
        return Ok(result);
    }
}
