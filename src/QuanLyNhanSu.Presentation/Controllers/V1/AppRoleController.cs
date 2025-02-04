using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
public class AppRoleController : ApiController
{
    public AppRoleController(ISender _sender) : base(_sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<Contract.Service.V1.AppRole.Response.GetAppRoleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllRole()
    {
        var result = await Sender.Send(new Contract.Service.V1.AppRole.Query.GetAppRoleQuery());
        return Ok(result);
    }
}
