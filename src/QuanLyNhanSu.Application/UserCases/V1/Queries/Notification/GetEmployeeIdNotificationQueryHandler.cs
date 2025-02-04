using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Notification;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Application.UserCases.V1.Queries.Notification;
public sealed class GetEmployeeIdNotificationQueryHandler : IQueryHandler<Query.GetNotificationByIdQuery, List<Response.NotificationResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryBase<Domain.Entities.Notification, Guid> _notificationRepository;
    private readonly IRepositoryBase<Domain.Entities.Employee, Guid> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetEmployeeIdNotificationQueryHandler
    (
        IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository,
        IRepositoryBase<Domain.Entities.Employee, Guid> employeeRepository,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    )
    {
        _notificationRepository = notificationRepository;
        _employeeRepository = employeeRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<Result<List<Response.NotificationResponse>>> Handle(Query.GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var user = _httpContextAccessor.HttpContext?.User
           ?? throw new Exception("Người dùng không xác thực.");

        var userId = _userManager.GetUserId(user);

        var appUser = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception("Người dùng không tồn tại.");

        var employeeId = appUser.EmployeeId;

        var employee = await _employeeRepository.FindAll(x => x.Id == employeeId && x.Status == Domain.Enumerations.StatusEnums.Status.Active).ToListAsync()
            ?? throw new Exception("Nhân viên không tồn tại.");

        var notifications = await _notificationRepository.FindAll(x => x.EmployeeId == employeeId).ToListAsync();
        notifications= notifications.OrderByDescending(x => x.CreatedAt).ToList();

        var result = _mapper.Map<List<Response.NotificationResponse>>(notifications);

        return Result.Success(result);
    }
}
