using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Service.V1.Enum;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
public class EnumController : ApiController
{
    public EnumController(ISender _sender) : base(_sender)
    {
    }

    [HttpGet("enum-type-of-allowance")]
    public async Task<IActionResult> GetEnumTypeOfAllowance()
    {
        var result = await Sender.Send(new Query.GetEnumTypeOfAllowanceQuery());
        return Ok(result);
    }

    [HttpGet("enum-type-of-penalty")]
    public async Task<IActionResult> GetEnumTypeOfPenalty()
    {
        var result = await Sender.Send(new Query.GetEnumTypeOfPenaltyQuery());
        return Ok(result);
    }

    [HttpGet("enum-pending-approval")]
    public async Task<IActionResult> GetEnumPendingApproval()
    {
        var result = await Sender.Send(new Query.GetEnumPendingApprovalQuery());
        return Ok(result);
    }

    [HttpGet("enum-status")]
    public async Task<IActionResult> GetEnumStatus()
    {
        var result = await Sender.Send(new Query.GetEnumStatusQuery());
        return Ok(result);
    }
}
