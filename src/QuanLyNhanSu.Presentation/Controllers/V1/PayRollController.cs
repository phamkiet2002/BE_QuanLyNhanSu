using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Service.V1.PayRoll;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
public class PayRollController : ApiController
{
    public PayRollController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayRolls(Command.CreatePayRollCommand createPayRoll)
    {
        var result = await Sender.Send(createPayRoll);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePayRolls(Command.CalculatePayrollResponseCommand calculatePayroll)
    {
        var result = await Sender.Send(calculatePayroll);
        return Ok(result);
    }
}
