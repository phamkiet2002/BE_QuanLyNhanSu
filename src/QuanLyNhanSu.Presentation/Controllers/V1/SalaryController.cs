using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Salary;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;

public class SalaryController : ApiController
{
    public SalaryController(ISender _sender) : base(_sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.SalaryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllSalary(string? SortOrder = null, int PageIndex = 1, int PageSize = 10)
    {
        var result = await Sender.Send(new Query.GetSalaryQuery(SortOrder, PageIndex, PageSize));
        return Ok(result);
    }
}
