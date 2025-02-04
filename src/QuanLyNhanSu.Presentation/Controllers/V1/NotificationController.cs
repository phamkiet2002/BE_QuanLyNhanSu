using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Notification;
using QuanLyNhanSu.Presentation.Abstractions;

namespace QuanLyNhanSu.Presentation.Controllers.V1;
public class NotificationController : ApiController
{
    public NotificationController(ISender _sender) : base(_sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<Contract.Service.V1.Notification.Response.NotificationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetNotificationById()
    {
        var result = await Sender.Send(new Query.GetNotificationByIdQuery());
        return Ok(result);
    }

    [HttpPut("{notificationId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> IsReadNotification(Guid notificationId)
    {
        var updateNotificationCommand = new Command.IsReadNotificationCommand(notificationId);
        var result = await Sender.Send(updateNotificationCommand);
        return Ok(result);
    }
}
