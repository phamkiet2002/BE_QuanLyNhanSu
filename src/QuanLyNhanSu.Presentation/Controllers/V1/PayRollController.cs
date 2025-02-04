using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.PayRoll;
using QuanLyNhanSu.Presentation.Abstractions;
using static QuanLyNhanSu.Contract.Service.V1.PayRoll.Command;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class PayRollController : ApiController
{
    public PayRollController(ISender _sender) : base(_sender) { }

    //[HttpPost]
    //public async Task<IActionResult> CreatePayRolls(Command.CreatePayRollCommand createPayRoll)
    //{
    //    var result = await Sender.Send(createPayRoll);
    //    return Ok(result);
    //}

    [HttpPut]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    public async Task<IActionResult> UpdatePayRolls(Command.CalculatePayrollResponseCommand calculatePayroll)
    {
        var result = await Sender.Send(calculatePayroll);
        return Ok(result);
    }

    [HttpPut("paid/{payrollId}")]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    public async Task<IActionResult> UpdatePayRolls(Guid payrollId, [FromBody] Command.UpdatePaidPayRollCommand command)
    {
        var updatePayRollCommand = new Command.UpdatePaidPayRollCommand(payrollId, command.PayRollStatus, command.ReasonNote);
        var result = await Sender.Send(updatePayRollCommand);
        return Ok(result);
    }
}
