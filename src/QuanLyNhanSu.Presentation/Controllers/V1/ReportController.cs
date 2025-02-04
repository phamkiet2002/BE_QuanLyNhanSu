using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Employee;
using QuanLyNhanSu.Infrastructure.EPPlus.Abstractions;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
public class ReportController : ApiController
{
    private readonly IRequestHandler<Query.GetEmployeesMapToAttendanceQuery, Result<PagedResult<Response.EmployeeMapToAttendanceResponse>>> _attendanceHandler;
    private readonly IExportAttendanceService _exportAttendanceService;

    private readonly IRequestHandler<Query.GetEmplpyeePayRollQuery, Result<PagedResult<Response.EmployeePayRollResponse>>> _payRollHandler;
    private readonly IExportPayRollEmployeeService _exportPayRollEmployeeService;

    private readonly IRequestHandler<Query.GetEmployeesQuery, Result<PagedResult<Response.EmployeeResponse>>> _employeeHandler;
    private readonly IExportEmployeeService _exportEmployeeService;

    public ReportController
    (
        ISender _sender,
        IRequestHandler<Query.GetEmployeesMapToAttendanceQuery, Result<PagedResult<Response.EmployeeMapToAttendanceResponse>>> attendanceHandler,
        IExportAttendanceService exportAttendanceService,
        IRequestHandler<Query.GetEmplpyeePayRollQuery, Result<PagedResult<Response.EmployeePayRollResponse>>> payRollHandler,
        IExportPayRollEmployeeService exportPayRollEmployeeService,
        IRequestHandler<Query.GetEmployeesQuery, Result<PagedResult<Response.EmployeeResponse>>> employeeHandler,
        IExportEmployeeService exportEmployeeService
    ) : base(_sender)
    {
        _attendanceHandler = attendanceHandler;
        _exportAttendanceService = exportAttendanceService;
        _payRollHandler = payRollHandler;
        _exportPayRollEmployeeService = exportPayRollEmployeeService;
        _employeeHandler = employeeHandler;
        _exportEmployeeService = exportEmployeeService;
    }

    [HttpGet("export-attendance")]
    public async Task<IActionResult> ExportAttendance([FromQuery] Query.GetEmployeesMapToAttendanceQuery query)
    {
        var result = await _attendanceHandler.Handle(query, CancellationToken.None);
        if (!result.IsSuccess || result.Value == null)
        {
            return BadRequest("Unable to fetch attendance data.");
        }

        var fileBytes = await _exportAttendanceService.ExportAttendanceToExcel(query, CancellationToken.None);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AttendanceReport.xlsx");
    }

    [HttpGet("export-payroll")]
    public async Task<IActionResult> ExportPayRoll([FromQuery] Query.GetEmplpyeePayRollQuery query)
    {
        var result = await _payRollHandler.Handle(query, CancellationToken.None);
        if (!result.IsSuccess || result.Value == null)
        {
            return BadRequest("Unable to fetch attendance data.");
        }

        var fileBytes = await _exportPayRollEmployeeService.ExportPayRollEmployeeToExcel(query, CancellationToken.None);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PayRollReport.xlsx");
    }

    [HttpGet("export-employee")]
    public async Task<IActionResult> ExportEmployee([FromQuery] Query.GetEmployeesQuery query)
    {
        var result = await _employeeHandler.Handle(query, CancellationToken.None);
        if (!result.IsSuccess || result.Value == null)
        {
            return BadRequest("Unable to fetch employee data.");
        }

        var fileBytes = await _exportEmployeeService.ExportEmployeeToExcel(query, CancellationToken.None);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee.xlsx");
    }
}
