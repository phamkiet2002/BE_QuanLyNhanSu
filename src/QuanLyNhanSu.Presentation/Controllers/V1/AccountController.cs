using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Service.V1.Account;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
public class AccountController : ApiController
{
    public AccountController(ISender _sender) : base(_sender)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Command.LoginCommand command)
    {
        try
        {
            var token = await Sender.Send(command);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await Sender.Send(new Command.LogoutCommand());
            return Ok(new { message = "Logout successful." });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
