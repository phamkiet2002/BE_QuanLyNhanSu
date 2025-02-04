using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.Notification;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Abstractions.Repositories;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.Notification;
public sealed class IsReadNotificationCommandHandler : ICommandHandler<Command.IsReadNotificationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.Notification, Guid> _notificationRepository;

    public IsReadNotificationCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.Notification, Guid> notificationRepository)
    {
        _unitOfWork = unitOfWork;
        _notificationRepository = notificationRepository;
    }

    public async Task<Result> Handle(Command.IsReadNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.FindByIdAsync(request.Id)
            ?? throw new Exception("Thông báo không tồn tại.");
        notification.MarkAsRead();
        _notificationRepository.Update(notification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
