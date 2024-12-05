using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
public class AttendanceController : ApiController
{
    public AttendanceController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckInAttendance([FromBody] Contract.Service.V1.Attendance.Command.CheckInAttendanceCommand command)
    {
        var result = await Sender.Send(command);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<Contract.Service.V1.Employee.Response.EmployeeMapToAttendanceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAllowance(int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Contract.Service.V1.Employee.Query.GetEmployeesMapToAttendanceQuery(PageIndex, PageSize));
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckOutAttendance([FromBody] Contract.Service.V1.Attendance.Command.CheckOutAttendanceCommand command)
    {  
        var result = await Sender.Send(command);
        return Ok(result);
    }
}
