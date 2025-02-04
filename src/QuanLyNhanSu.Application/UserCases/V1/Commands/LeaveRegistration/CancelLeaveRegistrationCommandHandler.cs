using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Abstractions.Shared;
using QuanLyNhanSu.Contract.Service.V1.LeaveRegistration;
using QuanLyNhanSu.Domain.Abstractions.Repositories;
using QuanLyNhanSu.Domain.Abstractions;
using QuanLyNhanSu.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSu.Domain.Entities.Identity;

namespace QuanLyNhanSu.Application.UserCases.V1.Commands.LeaveRegistration;
public sealed class CancelLeaveRegistrationCommandHandler : ICommandHandler<Command.CancelLeaveRegistrationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> _leaveRegistrationRepository;

    public CancelLeaveRegistrationCommandHandler
    (
        IUnitOfWork unitOfWork, IRepositoryBase<Domain.Entities.LeaveRegistration, Guid> leaveRegistrationRepository)
    {
        _unitOfWork = unitOfWork;
        _leaveRegistrationRepository = leaveRegistrationRepository;
    }

    public async Task<Result> Handle(Command.CancelLeaveRegistrationCommand request, CancellationToken cancellationToken)
    {
        var leaveRegistration = await _leaveRegistrationRepository.FindByIdAsync(request.Id)
            ?? throw new LeaveRegistrationException.LeaveRegistrationNotFound(request.Id);

        if
        (
            leaveRegistration.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Daduyet ||
            leaveRegistration.PendingApproval == Domain.Enumerations.ApproveEmuns.PendingApproval.Tuchoi
        )
            throw new Exception("không thể hủy vì đơn của bạn đã được xử lý. Liên hệ qlns để giải quyết.");

        leaveRegistration.CancelLeaveRegistration();

        _leaveRegistrationRepository.Update(leaveRegistration);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
