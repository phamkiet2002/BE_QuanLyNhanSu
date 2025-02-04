using System.Threading;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Application.Abstractions;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Domain.Exceptions;

namespace QuanLyNhanSu.Infrastructure.SignalR.Implementations;
public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IRepositoryBase<Domain.Entities.Notification, Guid> _notificationrepositoryBase;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly IRepositoryBase<Domain.Entities.Position, Guid> _positionRepository;

    public NotificationService
    (
        IHubContext<NotificationHub> hubContext,
        IRepositoryBase<Domain.Entities.Notification, Guid> notificationrepositoryBase,
        IUnitOfWork unitOfWork
,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        IRepositoryBase<Domain.Entities.Position, Guid> positionRepository)
    {
        _hubContext = hubContext;
        _notificationrepositoryBase = notificationrepositoryBase;
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
        _positionRepository = positionRepository;
    }

    public async Task SendNotificationToAdmins(string message, List<string> roles, string url, string title)
    {
        var employeesWithRoles = await _employeeRepository
            .FindAll(employee => employee.Status == Domain.Enumerations.StatusEnums.Status.Active)
            .Where(employee => employee.EmployeePositions
                .Any(position => position.Position.PositionRoles
                    .Any(role => roles.Contains(role.AppRole.NormalizedName))))
            .Select(employee => new
            {
                EmployeeId = employee.Id,
                Roles = employee.EmployeePositions
                    .SelectMany(position => position.Position.PositionRoles)
                    .Select(role => role.AppRole.NormalizedName)
            })
            .ToListAsync();

        foreach (var employeeId in employeesWithRoles)
        {
            var notification = Domain.Entities.Notification.CreateNotification(employeeId.EmployeeId, message, url, title, true);
            _notificationrepositoryBase.Add(notification);
        }

        await _unitOfWork.SaveChangesAsync();
        var sendTasks = roles.Select(role =>
           _hubContext.Clients.Group(role).SendAsync("ReceiveNotification", message, title)
       );
        await Task.WhenAll(sendTasks);
    }

    public async Task SendNotificationToUsers(Guid employeeId, string message, string url, string title)
    {
        var employeeById = await _employeeRepository.FindByIdAsync(employeeId)
            ?? throw new EmployeeException.EmployeeNotFoundException(employeeId);

        var notification = Domain.Entities.Notification.CreateNotification(employeeById.Id, message, url, title, false);
        _notificationrepositoryBase.Add(notification);
        await _unitOfWork.SaveChangesAsync();

        var userId = employeeById.AppUser.Id;

        await _hubContext.Clients.User(userId.ToString())
        .SendAsync("ReceiveNotificationForUser", message, title);
    }
}
