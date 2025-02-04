using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Infrastructure.SignalR;
public class NotificationHub : Hub
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;

    public NotificationHub(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _employeeRepository = employeeRepository;
    }

    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }

    public async Task SendNotificationToUser(string userId, string message)
    {
        await Clients.User(userId.ToString()).SendAsync("ReceiveNotificationForUser", message);
    }

    public override async Task OnConnectedAsync()
    {
        var user = Context.User;

        if (user?.Identity?.IsAuthenticated == true)
        {

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Hoặc bạn có thể dùng cách khác để lấy userId
            if (userId != null)
            {
                // Tìm employeeId từ userId
                var employee = await _employeeRepository.FindAll(e => e.AppUser.Id == userId).FirstOrDefaultAsync();
                if (employee != null)
                {
                    var employeeId = employee.Id;
                    await Groups.AddToGroupAsync(Context.ConnectionId, employeeId.ToString());
                }
            }

            var roles = user.Claims
                .Where(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                .Select(claim => claim.Value)
                .ToList();
            foreach (var role in roles)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, role);
            }
        }
        else
        {
            Console.WriteLine("Unauthenticated user connected.");
        }

        await base.OnConnectedAsync();
    }

    //public override async Task OnDisconnectedAsync(Exception? exception)
    //{
    //    var userName = Context.User?.Identity?.Name ?? "Unknown user";
    //    Console.WriteLine($"User disconnected: {userName}");
    //    await base.OnDisconnectedAsync(exception);
    //}
}
