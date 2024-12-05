using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.EmployeeLevel;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;

public class EmployeeLevelController : ApiController
{
    public EmployeeLevelController(ISender _sender) : base(_sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.EmployeeLevelResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllEmployeeLevel(int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetEmployeeLevelsQuery(PageIndex, PageSize));
        return Ok(result);
    }
}
