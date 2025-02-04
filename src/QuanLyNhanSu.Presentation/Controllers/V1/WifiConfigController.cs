using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.WifiConfig;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
public class WifiConfigController : ApiController
{
    public WifiConfigController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateWifiConfig([FromBody] Command.CreateWifiConfigCommand createWifiConfig)
    {
        var result = await Sender.Send(createWifiConfig);
        return Ok(result);
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,HR_MANAGER")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.WifiConfigResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetWifiConfig([FromQuery] Query.GetWifiConfigQuery getWifiConfig)
    {
        var result = await Sender.Send(getWifiConfig);
        return Ok(result);
    }
}
