using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuanLyNhanSu.Presentation.Abstractions;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Domain.Enumerations;
namespace QuanLyNhanSu.Presentation.Controllers.V1;

public class EmployeeController : ApiController
{
    public EmployeeController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Response.EmployeeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployee([FromBody] Command.CreateEmployeeCommand command)
    {
        var result = await Sender.Send(command);
        return Ok(result);
    }

    //[HttpGet("getpayroll")]
    //[ProducesResponseType(typeof(Result<IEnumerable<Response.EmployeePayRollResponse>>), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetAllEmployeePảyoll(
        
    //    int PageIndex = 1, int PageSize = 10
    //    )
    //{
    //    var result = await Sender.Send(new Query.GetEmplpyeePayRollQuery(
           
    //        PageIndex, PageSize
    //        ));
    //    return Ok(result);
    //}

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.EmployeeResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllEmployee(
        string? SearchTerm = null,
        string? WorkPlaceName = null, string? DepartmentName = null, string? PositionName = null, string? LevelName = null, StatusEnums.Status? Status = null,
        string? SortOrder = null,
        int PageIndex = 1, int PageSize = 10
        )
    {
        var result = await Sender.Send(new Query.GetEmployeesQuery(
            SearchTerm,
            WorkPlaceName, DepartmentName, PositionName, LevelName, Status,
            SortOrder,
            PageIndex, PageSize
            ));
        return Ok(result);
    }

    [HttpGet("getmanv/{MaNV}")]
    [ProducesResponseType(typeof(Result<Response.EmployeeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmployeeByMaNV(string MaNV = null)
    {
        var result = await Sender.Send(new Query.GetEmployeeByMaNVQuery(MaNV));
        return Ok(result);
    }

    [HttpGet("getid")]
    [ProducesResponseType(typeof(Result<Response.EmployeeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmployeeById(Guid employeeId)
    {
        var result = await Sender.Send(new Query.GetEmployeeByIdQuery(employeeId));
        return Ok(result);
    }

    [HttpPut("update/{employeeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmployee(Guid employeeId, [FromBody] Command.UpdateEmployeeCommand updateEmployee)
    {
        var updateEmployeeCommand = new Command.UpdateEmployeeCommand
        (
            employeeId, updateEmployee.Name, updateEmployee.Email, updateEmployee.Phone, updateEmployee.IdentityCard, updateEmployee.Gender,
            updateEmployee.DateOfBirth, updateEmployee.Address, updateEmployee.BankName, updateEmployee.BankAccountNumber
        );
        var result = await Sender.Send(updateEmployeeCommand);
        return Ok(result);
    }

    [HttpPut("employeelevel/{employeeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmployeeLevel(Guid employeeId, [FromBody] Command.UpdateEmployeeLevelCommand updateEmployeeLevel)
    {
        var updateEmployeeLevelCommand = new Command.UpdateEmployeeLevelCommand(employeeId, updateEmployeeLevel.LevelId);
        var result = await Sender.Send(updateEmployeeLevelCommand);
        return Ok(result);
    }

    [HttpPut("employeeworkplace/{employeeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmployeeWorkPlace(Guid employeeId, [FromBody] Command.UpdateEmployeeWorkPlaceCommand updateEmployeeWorkPlace)
    {
        var updateEmployeeWorkPlaceCommand = new Command.UpdateEmployeeWorkPlaceCommand(employeeId, updateEmployeeWorkPlace.WorkPlaceId, updateEmployeeWorkPlace.DepartmentId);
        var result = await Sender.Send(updateEmployeeWorkPlaceCommand);
        return Ok(result);
    }

    [HttpPut("employeeposition/{employeeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmployeePosition(Guid employeeId, [FromBody] Command.UpdateEmployeePositionCommand updateEmployeePosition)
    {
        var updateEmployeePositionCommand = new Command.UpdateEmployeePositionCommand(employeeId, updateEmployeePosition.PositionId);
        var result = await Sender.Send(updateEmployeePositionCommand);
        return Ok(result);
    }

    [HttpPut("employeesalary/{employeeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmployeeSalary(Guid employeeId, [FromBody] Command.UpdateEmployeeSalaryCommand updateEmployeeSalary)
    {
        var updateEmployeeSalaryCommand = new Command.UpdateEmployeeSalaryCommand(employeeId, updateEmployeeSalary.Salarys);
        var result = await Sender.Send(updateEmployeeSalaryCommand);
        return Ok(result);
    }

    [HttpPut("employeedepartment/{employeeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmployeeDepartment(Guid employeeId, [FromBody] Command.UpdateEmployeeDepartmentCommand updateEmployeeDepartment)
    {
        var updateEmployeeDepartmentCommand = new Command.UpdateEmployeeDepartmentCommand(employeeId, updateEmployeeDepartment.DepartmentId);
        var result = await Sender.Send(updateEmployeeDepartmentCommand);
        return Ok(result);
    }

    [HttpPut("leavework")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LeaveWorkEmployee(Guid employeeId)
    {
        var leaveWorkEmployeeCommand = new Command.LeaveWorkEmployeeCommand(employeeId);
        var result = await Sender.Send(leaveWorkEmployeeCommand);
        return Ok(result);
    }
}
