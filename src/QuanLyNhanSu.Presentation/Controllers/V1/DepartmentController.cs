using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Department;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
[Authorize]
public class DepartmentController : ApiController
{
    public DepartmentController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDepartment([FromBody] Command.CreateDepartmentCommand createDepartment)
    {
        var result = await Sender.Send(createDepartment);
        return Ok(result);
    }


    [HttpGet]
    [Authorize(Roles = "ADMIN,DEPARTMENT_MANAGER,HR_MANAGER")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.DepartmentResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllDepartment(string SearchTerm = null, string? WorkPlaceName = null, string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetDepartmentsQuery(SearchTerm, WorkPlaceName, SortOrder, PageIndex, PageSize));
        return Ok(result);
    }

    [HttpPut("{departmentId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDepartment(Guid departmentId, [FromBody] Command.UpdateDepartmentCommand updateDepartment)
    {
        var updateDepartmentCommand = new Command.UpdateDepartmentCommand
        (
            departmentId, updateDepartment.Name, updateDepartment.Description,
            updateDepartment.WorkPlaceId, updateDepartment.WorkSheduleId
        );
        var result = await Sender.Send(updateDepartmentCommand);
        return Ok(result);
    }

    [HttpDelete("{departmentId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteDepartment(Guid departmentId)
    {
        var result = await Sender.Send(new Command.DeleteDepartmentCommand(departmentId));
        return Ok(result);
    }
}
